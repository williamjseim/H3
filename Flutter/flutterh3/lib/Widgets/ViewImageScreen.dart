import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/main.dart';

class ImageView extends StatefulWidget{
  @override
  State<ImageView> createState() => _ImageViewState();
}

class _ImageViewState extends State<ImageView> {
  @override
  Widget build(BuildContext context) {
    return Center(
          child:  Scaffold(
            appBar: mainappbar(context),
            body: Container(
              color: Colors.amber,
              child: LayoutBuilder(builder: ((context, constraints) {
                Widget inkwell = Container();
                return Column(
                  children: [
                    Container(
                      height: constraints.maxHeight * 0.9,
                      color: Colors.red,
                      child: DragTarget<Widget>(builder: (context, objects, dynamics) {
                        return inkwell;
                      },
                        onAcceptWithDetails: (details){ print("success"); inkwell = InkWell(child: details.data); },
                        onWillAcceptWithDetails: (details) {
                          return true;
                        },
                      ),
                      
                    ),
                    Container(
                      height: constraints.maxHeight * 0.1,
                      color: Colors.green,
                      child: ListView.builder(
                        itemCount: 5,
                        scrollDirection: Axis.horizontal,
                        itemBuilder: (context, index) {
                          return Container(
                            child: FutureBuilder(future: image(), builder: (context, snapshot){
                              if(snapshot.data == null){
                                return Icon(Icons.recycling);
                              }
                              else{
                                return snapshot.data!;
                              }
                            }),
                          );
                        },
                      ),
                    ),
                  ],
                );
              })),
            )
          ),
        );
  }

  Future<Widget> image() async {
    //var bytes = await FlutterSecureStorage().read(key: "Image") as String;
    //var imagebytes = Base64Decoder().convert(bytes);
    Image image = Image.asset("favicon.png");
    return Draggable<Widget>(
      feedback: image,
      childWhenDragging: Container(
        height: 100,
        width: 100,
        child: DecoratedBox(
          decoration: BoxDecoration(color: Colors.black),
        ),
      ),
      data: image,
      child: image,
    );
  }
}