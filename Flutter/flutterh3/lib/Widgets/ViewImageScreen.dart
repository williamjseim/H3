import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/main.dart';

class ImageView extends StatefulWidget{
  @override
  State<ImageView> createState() => _ImageViewState();
}

class _ImageViewState extends State<ImageView> {
  @override
  Widget build(BuildContext context) {
  late double screenHeight = MediaQuery.of(context).size.height;
  late double screenWidth = MediaQuery.of(context).size.width;
    Widget? test;
    return Center(
          child:  Scaffold(
            appBar: mainappbar(context),
            body: Column(
              children: [
                SizedBox(
                  height: screenHeight * 0.4,
                  child: Column(
                    children: [
                      DecoratedBox(decoration: BoxDecoration(color: Colors.amber))
                    ],
                  ),
                ),
                SizedBox(
                  height: screenHeight * 0.4,
                  child: ListView.builder(
                    scrollDirection: Axis.horizontal,
                    itemBuilder: (context, index) {
                      FutureBuilder(
                        future: image(),
                        builder: (context, snapshot) {
                          if(snapshot.data != null){
                            return snapshot.data!;
                          }
                          else{
                            return CircularProgressIndicator();
                          }
                      },);
                    },
                  ),
                )
              ],
            ),

          ),
    );
  }

  Future<Widget> image() async{
    var bytes = await FlutterSecureStorage().read(key: "Image") as String;
    var imagebytes = Base64Decoder().convert(bytes);
    Image image = Image.memory(imagebytes);
    return Draggable<Widget>(
      feedback: image,
      childWhenDragging: const SizedBox(
        width: 100,
        height: 200,
        child: DecoratedBox(
          decoration: BoxDecoration(color: Colors.black),
        ),
      ),
      data: image,
      child: image,
      );
  }
}