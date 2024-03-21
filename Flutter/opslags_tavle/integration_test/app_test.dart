
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:opslags_tavle/main.dart';

void main(){
  group("integration tests", () { 
    IntegrationTestWidgetsFlutterBinding.ensureInitialized();

    testWidgets("Image taking test", (widgetTester) async {

      await widgetTester.pumpWidget(const MyApp());
      
      await widgetTester.pumpAndSettle(Duration(seconds: 5));

      expect(find.text("Camera"), findsOneWidget);
      
      final fab = find.text("Camera");

      await widgetTester.tap(fab);

      await widgetTester.pumpAndSettle(Duration(seconds: 5));

      final camera = find.byIcon(Icons.camera);

      await widgetTester.tap(camera);

      await widgetTester.pumpAndSettle(Duration(seconds: 2));// */

      final boardFab = find.text("Board");
      
      await widgetTester.tap(boardFab);

      await widgetTester.pumpAndSettle(const Duration(seconds: 2));

      final imageMenuFab = find.byIcon(Icons.menu);

      await widgetTester.tap(imageMenuFab);

      await widgetTester.pumpAndSettle(const Duration(seconds: 2));

      final images = find.byType(GestureDetector);

      await widgetTester.tap(images.first);

      await widgetTester.pumpAndSettle(const Duration(seconds: 2));
    });
  });
}