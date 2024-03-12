import 'package:flutter/material.dart';
import 'package:flutterh3/Provider/counterProvider.dart';
import 'package:flutterh3/Widgets/BlocScreen.dart';
import 'package:flutterh3/Widgets/ProviderWidget.dart';
import 'package:flutterh3/Widgets/RandomScreen.dart';
import 'package:flutterh3/Widgets/ViewImageScreen.dart';
import 'package:go_router/go_router.dart';
import 'package:provider/provider.dart';
import 'package:flutterh3/Widgets/CameraScreen.dart';

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

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
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

class _MyHomePageState extends State<MyHomePage> {
  CounterProvider? provider;
  void _incrementCounter() {
    provider!.increment();
  }

  void _decrementCounter() {
    if (provider!.counter.counter > 0) {
      provider!.decrement();
    }
  }

  @override
  Widget build(BuildContext context) {
    provider = context.read<CounterProvider>();
    return MaterialApp.router(routerConfig: _router);
  }
}

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

AppBar mainappbar(BuildContext context) => AppBar(
  shadowColor: const Color.fromARGB(255, 9, 255, 0),
  // TRY THIS: Try changing the color here to a specific color (to
  // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
  // change color while the other colors stay the same.
  backgroundColor: const Color.fromARGB(255, 255, 0, 0),
  // Here we take the value from the MyHomePage object that was created by
  // the App.build method, and use it to set our appbar title.
  title: const Text("Title"),
  leading: FloatingActionButton(
    onPressed: () {
      context.go('/');
    },
    backgroundColor: const Color.fromARGB(0, 0, 0, 0),
    child: const Icon(Icons.keyboard_return_rounded),
  ),
);

Drawer mainappdrawer(BuildContext context) => Drawer(
      backgroundColor: const Color.fromARGB(0, 255, 255, 255),
      shadowColor: const Color.fromARGB(255, 9, 255, 0),
      child: ListView(
        scrollDirection: Axis.vertical,
        children: [
          const DrawerHeader(child: Text("data")),
          ListTile(
            leading: const Icon(Icons.home),
            title: const Text("Home"),
            onTap: () => context.go('/'),
          ),
          ListTile(
            leading: const Icon(Icons.block),
            title: const Text("Provider"),
            onTap: () => context.go('/Provider'),
          ),
          ListTile(
            leading: const Icon(Icons.account_tree),
            title: const Text("Bloc"),
            onTap: () => context.go('/Bloc'),
          ),
          ListTile(
            leading: const Icon(Icons.door_back_door),
            title: const Text("Random"),
            onTap: () => context.go('/Random'),
          ),
          ListTile(
            leading: const Icon(Icons.door_back_door),
            title: const Text("Camera"),
            onTap: () => context.go('/Camera'),
          ),
          ListTile(
            leading: const Icon(Icons.door_back_door),
            title: const Text("Image"),
            onTap: () => context.go('/Image'),
          ),
        ],
      ),
    );

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
        drawer: mainappdrawer(context)
        /*floatingActionButton: FloatingActionButton(
          onPressed: _incrementCounter,
          tooltip: 'Increment',
          child: const Icon(Icons.add),
        ), // This trailing comma makes auto-formatting nicer for build methods.*/
        );
  }
}
