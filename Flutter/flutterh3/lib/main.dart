import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutterh3/Provider/counterProvider.dart';
import 'package:flutterh3/Screens/BlocScreen.dart';
import 'package:flutterh3/Screens/ProviderWidget.dart';
import 'package:flutterh3/Screens/RandomScreen.dart';
import 'package:flutterh3/Screens/ViewImageScreen.dart';
import 'package:flutterh3/Widgets/mainComponents.dart';
import 'package:go_router/go_router.dart';
import 'package:provider/provider.dart';
import 'package:flutterh3/Screens/CameraScreen.dart';

void main() {
  runApp(MaterialApp.router(
    routerConfig: _router,
  ));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      routerConfig: _router,
    );
  }
}

final GoRouter _router = GoRouter(routes: [
  GoRoute(
    path: '/',
    builder: (BuildContext context, GoRouterState state) {
      return const HomeScreen();
    },
  ),
  GoRoute(
    path: '/Provider',
    builder: (BuildContext context, GoRouterState state) {
      return const ProviderWidget();
    },
  ),
  GoRoute(
    path: '/Bloc',
    builder: (BuildContext context, GoRouterState state) {
      return const BlocScreen();
    },
  ),
  GoRoute(
    path: '/Random',
    builder: (BuildContext context, GoRouterState state) {
      return const RandomScreen();
    },
  ),
  GoRoute(
    path: '/Camera',
    builder: (BuildContext context, GoRouterState state) {
      return const Camera();
    },
  ),
  GoRoute(
    path: '/Image',
    builder: (BuildContext context, GoRouterState state) {
      return ImageView();
    },
  ),
]);

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          shadowColor: const Color.fromARGB(255, 9, 255, 0),
          // TRY THIS: Try changing the color here to a specific color (to
          // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
          // change color while the other colors stay the same.
          backgroundColor: const Color.fromARGB(255, 255, 0, 0),
          // Here we take the value from the MyHomePage object that was created by
          // the App.build method, and use it to set our appbar title.
          title: const Text("Title"),
        ),
        drawer: mainappdrawer(context),
        body: Container(
          child: LayoutBuilder(builder: (c, a) { return Container(
            color: Colors.black,
             child: kIsWeb ? Image.asset("assets/MainBackground.gif", height: a.maxHeight, width: a.maxWidth,): Image.asset("assets/giphy.gif", height: a.maxHeight, width: a.maxWidth,));}),
        ),
        /*floatingActionButton: FloatingActionButton(
          onPressed: _incrementCounter,
          tooltip: 'Increment',
          child: const Icon(Icons.add),
        ), // This trailing comma makes auto-formatting nicer for build methods.*/
        );
  }
}
