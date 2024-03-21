
import 'dart:convert';

import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:go_router/go_router.dart';
import 'package:opslags_tavle/Notifications/notifications.dart';
import 'package:opslags_tavle/Screens/board_screen.dart';
import 'package:opslags_tavle/Screens/camera_screen.dart';
import 'package:opslags_tavle/Widgets/custom_widgets.dart';
import 'package:opslags_tavle/firebase_options.dart';
import 'package:universal_io/io.dart';

@pragma('vm:entry-point')
Future<void> notificationTapBackground(RemoteMessage response) async{
  print(response.messageId);
}

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
);
  if(!kIsWeb){
    FirebaseMessaging.onBackgroundMessage((message) => notificationTapBackground(message));
  }
  var token = await FirebaseMessaging.instance.getToken();
  print(token);
  runApp(const MyApp());

}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      routerConfig: _router,
      title: 'Opslags tavle',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
    );
  }
}

bool _notificationsEnabled = false;

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key});

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {

  @override
  void initState() {
    super.initState();
    _isAndroidPermissionGranted();
    _requestPermissions();
    _configureDidReceiveLocalNotificationSubject();
    _configureSelectNotificationSubject();
    firebaseInit();
  }

  void firebaseInit() async{

    await FirebaseMessaging.instance.subscribeToTopic("Topic");
    FirebaseMessaging.onMessage.listen((message) async {
      RemoteNotification? notification = message.notification;
      if(Platform.isIOS){
        FirebaseMessaging.instance.setForegroundNotificationPresentationOptions(
          alert: true,
          badge: true,
          sound: true,
        );
      }
      
      if(kDebugMode){
        print(notification?.title);
        print(notification?.body);
        print(message.data.toString());
      }
      if(Platform.isAndroid){
        _initAndroidNotifications(message);
        _showNotification(message);
      }
    });
  }

  void _initAndroidNotifications(RemoteMessage message) async{
      var androidInitializationSettings = const AndroidInitializationSettings("@mipmap/ic_launcher");
      var iosInitiazialiaction = const DarwinInitializationSettings();

      var initializationSetting = InitializationSettings(
        android: androidInitializationSettings, iOS: iosInitiazialiaction,
      );

      await flutterLocalNotificationsPlugin.initialize(initializationSetting, onDidReceiveNotificationResponse: (details){
        _handleMessage(details, message);
      });
    }

    

  _handleMessage(NotificationResponse details, RemoteMessage message){
  }

  Future<void> _showNotification(RemoteMessage message) async{
    String channelId =  "1";
    AndroidNotificationChannel channel;
    if(message.notification?.android != null){
      print(message.notification!.android!.sound);

      channel = AndroidNotificationChannel(
        message.notification!.android!.channelId!,
        channelId,
        importance: Importance.max,
        showBadge: true,
        playSound: true,
        sound: RawResourceAndroidNotificationSound(message.notification!.android!.sound)
      );
    }
    else{
      channel = AndroidNotificationChannel(
        channelId,
        channelId,
        importance: Importance.max,
        showBadge: true,
        playSound: true,
        sound: RawResourceAndroidNotificationSound("motorsound")
      );
    }

    AndroidNotificationDetails details = AndroidNotificationDetails(channel.id.toString(), channel.name,
      importance: Importance.high,
      channelDescription: "description",
      playSound: true,
      ticker: 'ticker',
      sound: channel.sound,

    );

    NotificationDetails notidetails = NotificationDetails(android: details);

    Future.delayed(Duration.zero, (){
      flutterLocalNotificationsPlugin.show(0, message.notification!.title, message.notification!.body, notidetails);
    });
  }

  Future<void> _isAndroidPermissionGranted() async {
    if (Platform.isAndroid) {
      final bool granted = await flutterLocalNotificationsPlugin
              .resolvePlatformSpecificImplementation<
                  AndroidFlutterLocalNotificationsPlugin>()
              ?.areNotificationsEnabled() ??
          false;

      setState(() {
        _notificationsEnabled = granted;
      });
    }
  }

   Future<void> _requestPermissions() async {
    if (Platform.isIOS || Platform.isMacOS) {
      await flutterLocalNotificationsPlugin
          .resolvePlatformSpecificImplementation<
              IOSFlutterLocalNotificationsPlugin>()
          ?.requestPermissions(
            alert: true,
            badge: true,
            sound: true,
          );
      await flutterLocalNotificationsPlugin
          .resolvePlatformSpecificImplementation<
              MacOSFlutterLocalNotificationsPlugin>()
          ?.requestPermissions(
            alert: true,
            badge: true,
            sound: true,
          );
    } else if (Platform.isAndroid) {
      final AndroidFlutterLocalNotificationsPlugin? androidImplementation =
          flutterLocalNotificationsPlugin.resolvePlatformSpecificImplementation<
              AndroidFlutterLocalNotificationsPlugin>();

      final bool? grantedNotificationPermission =
          await androidImplementation?.requestNotificationsPermission();
      setState(() {
        _notificationsEnabled = grantedNotificationPermission ?? false;
      });
    }
  }

  void _configureSelectNotificationSubject() {
    selectNotificationStream.stream.listen((String? payload) async {
      await Navigator.of(context).push(MaterialPageRoute<void>(
        builder: (BuildContext context) {return Container();},
      ));
    });
  }

  void _configureDidReceiveLocalNotificationSubject() {
    didReceiveLocalNotificationStream.stream
        .listen((ReceivedNotification receivedNotification) async {
      await showDialog(
        context: context,
        builder: (BuildContext context) => CupertinoAlertDialog(
          title: receivedNotification.title != null
              ? Text(receivedNotification.title!)
              : null,
          content: receivedNotification.body != null
              ? Text(receivedNotification.body!)
              : null,
          actions: <Widget>[
            CupertinoDialogAction(
              isDefaultAction: true,
              onPressed: () async {
                Navigator.of(context, rootNavigator: true).pop();
                await Navigator.of(context).push(
                  MaterialPageRoute<void>(
                    builder: (BuildContext context) { return Container();}
                       
                  ),
                );
              },
              child: const Text('Ok'),
            )
          ],
        ),
      );
    });
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: PreferredSize(preferredSize: Size(100,104), child: Container(
        decoration: BoxDecoration(
          image: DecorationImage(repeat: ImageRepeat.repeatX, image: AssetImage("assets/koi.png")),
        ),
        child: Column(children: [
          mainAppBar(title: "Koi Images"),
          LayoutBuilder(
            builder: (context, constraints) {
              return OverflowBar(
                children: [
                  Container(width: constraints.maxWidth / 3, child: barButton(Route: "/", child:Text("Home", style: TextStyle(color: Colors.white),), backgroundColor: Colors.transparent),),
                  Container(width: constraints.maxWidth / 3, child: barButton(Route: "/Camera", child:Text("Camera", style: TextStyle(color: Colors.white),), backgroundColor: Colors.transparent),),
                  Container(width: constraints.maxWidth / 3, child: barButton(Route: "/Board", child:Text("Board", style: TextStyle(color: Colors.white),), backgroundColor: Colors.transparent),),
                ],
              );
            }
          )
        ],),
      ),),
      body: Container(
        color: Colors.black,
        child: Stack(
          children:[
            Center(
              child: Image.asset("assets/giphy.gif"),
            ),
            Container(
              child: Container(
                child: LoginForm(),
              ),
            )
          ],
        ),
      ),
    );
  }
}

final GoRouter _router = GoRouter(routes: [
  GoRoute(
    path: "/",
    builder: (context, state) {
      return const MyHomePage();
    },
  ),
  GoRoute(
    path: "/Camera",
    builder: (context, state) {
      return const CameraScreen();
    },
  ),
  GoRoute(
    path: "/Board",
    builder: (context, state) {
      return const BoardScreen();
    },
  ),
]);

class LoginForm extends StatelessWidget{
  LoginForm({super.key});
  @override
  String username = "";
  String password = "";
  Widget build(BuildContext context) {
    return Form(child: Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        TextFormField(onChanged: (value) { username = value; }, decoration: const InputDecoration(labelText: "Username"), initialValue: "", style: TextStyle(color: Colors.white), textAlign: TextAlign.center,),
        TextFormField(onChanged: (value) { password = value; }, decoration: const InputDecoration(labelText: "Username"), initialValue: "", style: TextStyle(color: Colors.white,), textAlign: TextAlign.center,),
        ElevatedButton(onPressed: () async{
          HttpClient client = HttpClient();
          Uri uri;
          if(Platform.isAndroid){
            uri = Uri.http("10.0.2.2:5142", "/Login", {"Username" : username, "Password" : password});
          }
          else{
            uri = Uri.http("localhost:5142", "/Login", {"Username" : username, "Password" : password});
          }
          var request = await client.postUrl(uri);
          HttpClientResponse response = await request.close();
          response.transform(utf8.decoder).join().then(
            (v) {print(v); }
          );
        }, child: Text("Login")),
      ],
    ));
  }

}