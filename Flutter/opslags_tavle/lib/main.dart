import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:opslags_tavle/Screens/board_screen.dart';
import 'package:opslags_tavle/Screens/camera_screen.dart';
import 'package:opslags_tavle/Widgets/custom_widgets.dart';

void main() {
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

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key});

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
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
        child: Center(
          child: Image.asset("assets/giphy.gif"),
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