// This is a basic Flutter widget test.
//
// To perform an interaction with a widget in your test, use the WidgetTester
// utility in the flutter_test package. For example, you can send tap and scroll
// gestures. You can also use WidgetTester to find child widgets in the widget
// tree, read text, and verify that the values of widget properties are correct.

import 'dart:js';

import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:flutterh3/Screens/ViewImageScreen.dart';
import 'package:flutterh3/Widgets/mainComponents.dart';

import 'package:flutterh3/main.dart';

void main() {
  testWidgets('Counter increments smoke test', (WidgetTester tester) async {
    // Build our app and trigger a frame.
    await tester.pumpWidget(const MyApp());

    expect(find.text("Title"), findsOneWidget);


  });
  
  testWidgets("Drag test on view image screen", (widgetTester) async {
    TestWidgetsFlutterBinding.ensureInitialized();
    await widgetTester.pumpWidget(MaterialApp(
      home: ImageView(),
    ));
    
    await widgetTester.pump( const Duration(seconds: 1));
    print(widgetTester.any(find.byType(Image)));
    expect(mainappbar, findsOneWidget);
    //await widgetTester.drag(find.image(AssetImage("favicon.png")), Offset(100.1, 100.1));

  });
}
