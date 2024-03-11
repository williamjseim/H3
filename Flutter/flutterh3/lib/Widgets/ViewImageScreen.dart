import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/main.dart';

class ImageView extends StatelessWidget{
  @override
  Widget build(BuildContext context) {
    return Center(
      child: FutureBuilder(
        future: image(),
        builder: (context, snapshot) {
          return Scaffold(
            appBar: mainappbar(context),
            body: Center(
              child: Column(
                children: [
                  snapshot.data!
                ],
              ),
            ),
          );
          },
        )
    );
  }

  Future<Image> image() async{
    var bytes = await FlutterSecureStorage().read(key: "Image") as String;
    var imagebytes = Base64Decoder().convert(bytes);
    return Image.memory(imagebytes);
  }


}