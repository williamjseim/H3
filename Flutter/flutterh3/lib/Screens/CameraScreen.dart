import 'dart:convert';
import 'package:flutter/services.dart';
import 'package:universal_io/io.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/Widgets/mainComponents.dart';
import 'package:flutterh3/main.dart';
import 'package:camera/camera.dart';

class Camera extends StatefulWidget{
  const Camera({super.key});

  @override
  State<Camera> createState() => _CameraState();
}

class _CameraState extends State<Camera> with WidgetsBindingObserver, TickerProviderStateMixin{
  CameraController? controller;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: mainappbar(context),
      body: Center(
        child: FutureBuilder(
          future: _cameraPreviewWidget(),
          builder: ( context, snapshot)
          {
            return Center(
              child: Column(
                children: [
                  if(snapshot.data != null)
                    snapshot.data!
                    else
                    Text("adswawd")
                ],
              ),
            );
          }
        ),
      ),
    );
  }


//flutter run -d chrome --web-browser-flag "--disable-web-security"
   Future _cameraPreviewWidget() async {
    var http = HttpClient();
    var cameras = await availableCameras();
    CameraController camera = CameraController(
      cameras[0],
      kIsWeb ? ResolutionPreset.high : ResolutionPreset.medium,
      enableAudio: false,
      imageFormatGroup:  ImageFormatGroup.jpeg,
    );
    await camera.initialize();
    return Column(
      children: [
        CameraPreview(
          camera,
        ),
        FloatingActionButton(
          onPressed: () async {
            var picture = await camera.takePicture();
            var bytes = await picture.readAsBytes();
            var base64 = base64Encode(bytes);
            var uri = Uri.http("10.0.2.2:5142", "/Image", {"Image" : base64});
            var request = await http.postUrl(uri);
            request.headers.add("Access-Control-Allow-Origin", "*");
            var response = await request.close();
            print(request);
          },
          child: const Icon(Icons.camera),
        )
      ],
    );
  }

  _initializeCamera(CameraDescription desc){
    final CameraController _controller = CameraController(
      desc,
      kIsWeb ? ResolutionPreset.high : ResolutionPreset.medium,
      enableAudio: false,
      imageFormatGroup:  ImageFormatGroup.jpeg,
    );

    controller = _controller;

  }

}