import 'dart:convert';
import 'dart:math';
import 'package:flutter/cupertino.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutterh3/Bloc/ImageBloc.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/Widgets/mainComponents.dart';

class ImageView extends StatefulWidget{
  const ImageView({super.key});

  @override
  State<ImageView> createState() => _ImageViewState();
}

class _ImageViewState extends State<ImageView> {
  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [BlocProvider(create: (context) => ImageBloc())],
      child: Scaffold(
        appBar: mainappbar(context),
        body: LayoutBuilder(builder: ((context, constraints) {
          return Column(
            children: [
              MouseRegion(
                child: DragTarget(
                  onWillAcceptWithDetails: (details) {
                    return true;
                  },
                  onAcceptWithDetails: (details) {
                  },
                  builder: (context, dyn, objects) {
                    return Container(
                      color: Colors.green,
                      height: constraints.maxHeight * 0.9,
                    child: BlocConsumer<ImageBloc, Images>(
                      listener: (context, dyn) { }, 
                      builder: (context, state){
                        print("builder");
                          print("Images");
                          print(state.images.length);
                          return Stack(
                            children: state.images,
                          );
                      }
                    ),
                  );
                }
                ),
              ),
              Container(
                height: constraints.maxHeight * 0.1,
                color: Colors.amber,
                child: ListView.builder(
                  scrollDirection: Axis.horizontal,
                  itemCount: 3,
                  itemBuilder: (context, index){
                  return FutureBuilder(future: image(context), builder: (context, snapshot){
                    if(snapshot.data == null){
                      return Container();
                    }
                    else{
                      return snapshot.data!;
                    }
                  });
                }),
              )
            ],
          );
        }),
        ),
      ),
    );
  }

  Future<Widget> image(BuildContext context) async {
    /*var bytes = await FlutterSecureStorage().read(key: "Image") as String;
    var imagebytes = Base64Decoder().convert(bytes);
    Image image = Image.memory(imagebytes);// */
    Image image = Image.asset("assets/favicon.png");
    return LongPressDraggable<Widget>(
      feedback: image,
      childWhenDragging: Container(
       child: DecoratedBox(
         decoration: BoxDecoration(color: Colors.black),
        ),
      ),
      onDragEnd: (details) {
        if(details.wasAccepted){
          context.read<ImageBloc>().add(NewImageEvent(Point(details.offset.dx, details.offset.dy), "assets/favicon.png"));
        }
      },
      data: image,
      child: image,
    );
  }
}