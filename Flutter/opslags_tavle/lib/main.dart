import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:icons_plus/icons_plus.dart';
import 'package:opslags_tavle/Screens/camera_screen.dart';

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
      appBar: mainAppBar(),
      body: Center(
        child: LayoutBuilder(
            builder: (context, constraints) {
              return Container(
                height: constraints.maxHeight,
                width: constraints.maxWidth,
                color: Colors.black87,
                child: Image.asset("assets/giphy.gif"),
              );
            }
          ),
        ),
        floatingActionButton: FloatingActionButton(onPressed: () => context.go("/Camera"), child: Icon(Icons.menu),),
      );
  }
}

AppBar mainAppBar({String title = "Missing Title"}){
  return AppBar(
    title: Text(title),
    backgroundColor: Colors.red,
  );
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
]);