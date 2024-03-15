// This is a basic Flutter widget test.
//
// To perform an interaction with a widget in your test, use the WidgetTester
// utility in the flutter_test package. For example, you can send tap and scroll
// gestures. You can also use WidgetTester to find child widgets in the widget
// tree, read text, and verify that the values of widget properties are correct.

import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:go_router/go_router.dart';
import 'package:opslags_tavle/Screens/camera_screen.dart';
import 'package:integration_test/integration_test.dart';

import 'package:opslags_tavle/main.dart';

void main() {
  testWidgets('Counter increments smoke test', (WidgetTester tester) async {
    // Build our app and trigger a frame.
    await tester.pumpWidget(const MyApp());
    expect(find.byIcon(Icons.menu), findsOneWidget);
    await tester.tap(find.byIcon(Icons.menu)); // menu button
    await tester.pump(Duration(seconds: 2));

    // Verify that our counter starts at 0.
  });

  testWidgets("Test Camera image taking", (widgetTester) async{
    await widgetTester.pumpWidget(MaterialApp(
      builder: (context, child) {
        return const CameraScreen();
      },
    ));
    await widgetTester.pump(Duration(seconds: 3)); // let page finish the artificial delay
    expect(find.byIcon(Icons.camera), findsOneWidget); // image taking button
  });

  
}
