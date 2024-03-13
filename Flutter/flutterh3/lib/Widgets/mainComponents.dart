import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

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