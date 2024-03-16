import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:opslags_tavle/Blocs/Image_bloc.dart';
import 'package:opslags_tavle/Blocs/board_bloc.dart';
import 'package:opslags_tavle/Widgets/custom_widgets.dart';
import 'package:opslags_tavle/main.dart';
import 'package:zoom_widget/zoom_widget.dart';

class BoardScreen extends StatelessWidget{
  const BoardScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [ BlocProvider(create: (context) => BoardBloc(BoardData())), BlocProvider(create: (context) => ImageBloc(ImageData())) ],
       child:  Scaffold(
          body: LayoutBuilder(
            builder: (context, constraints) {
              return Column(
                children: [
                  tabBar(height: constraints.maxHeight * 0.1),
                  Stack(
                    children: [
                      Container(
                        height: constraints.maxHeight * 0.9,
                        child: DragTarget(
                          builder: (context, objects, dynmics) {
                            return Zoom(
                              child: Container(
                                color: const Color.fromARGB(255, 153, 0, 0),
                                height: constraints.maxHeight * 0.9,
                                width: constraints.maxWidth,
                                child: BlocConsumer<BoardBloc, BoardData>(
                                  listener: (context, state) { },
                                  builder: (context, state) {
                                    return Stack(
                                      children: state.widgets,
                                    );
                                  },
                                ),
                              ),
                            );
                          },
                          ),
                        ),
                        BlocConsumer<BoardBloc, BoardData>(
                          listener: (context, state) {},
                          builder: (context, state) {
                            if(state.ImageWidgetOpen){

                            return Positioned(
                              bottom: 0,
                              child: Container(
                                height: 500,
                                width: constraints.maxWidth,
                                color: Colors.amber,
                                child: BlocConsumer<ImageBloc, ImageData>(
                                  listener: (context, state){},
                                  builder: (context, state) {
                                    return FutureBuilder(
                                      future: _ConstructImages(state.localImages),
                                      builder: (context, snapshot) {
                                        if(snapshot.hasData){
                                          return GridView.count(
                                            crossAxisCount: 3,
                                              children: [Image.asset("assets/koi.png")],
                                            );
                                        }
                                        return Center(child: CircularImageSpinner(duration: Duration(seconds: 5),),);
                                      }
                                    );
                                  },
                                )
                              ),
                            );
                            }
                            else{
                              return FloatingActionButton(onPressed: (){context.read<ImageBloc>().add(GetLocalImages()); context.read<BoardBloc>().add(OpenCloseEvent()); }, child: Icon(Icons.assessment),);
                            }
                          },
                        )
                    ],
                  ),
                    
                ],
              );
            }
          ),
        ),
    );
  }

}

Future<List<Image>> _ConstructImages(List<String> images) async{
  print("constructed ${images.length}");
    List<Image> list = List.empty();
    for (var i = 0; i < images.length; i++) {
     Image.memory(base64Decode(images[i])); 
    }
    return list;
  }