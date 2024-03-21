import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:opslags_tavle/Blocs/Image_bloc.dart';
import 'package:opslags_tavle/Blocs/board_bloc.dart';
import 'package:opslags_tavle/Widgets/custom_widgets.dart';
import 'package:zoom_widget/zoom_widget.dart';

class BoardScreen extends StatefulWidget{
  const BoardScreen({super.key});

  @override
  State<BoardScreen> createState() => _BoardScreenState();
}

class _BoardScreenState extends State<BoardScreen> {
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
                          onMove: (details) {
                            print("move");
                          },
                          onWillAcceptWithDetails: (details) { print("on will accept"); return true; },
                          builder: (context, objects, dynmics) {
                            print("dragtarget");
                            return Zoom(
                              child: Container(
                                color: const Color.fromARGB(255, 153, 0, 0),
                                height: constraints.maxHeight * 0.9,
                                width: constraints.maxWidth,
                                child: BlocConsumer<BoardBloc, BoardData>(
                                  buildWhen: (previous, current) {
                                    bool a = current.boardChanged;
                                    current.boardChanged = false;
                                    return a;
                                  },
                                  listener: (context, state) { },
                                  builder: (context, state) {
                                    return FutureBuilder(
                                      future: _boardWidgets(state.widgets),
                                      builder: (context, snapshot) {
                                        return Stack(
                                          children: snapshot.hasData ? snapshot.data! : [],
                                        );
                                      }
                                    );
                                  },
                                ),
                              ),
                            );
                          },
                          ),
                        ),
                        BlocConsumer<BoardBloc, BoardData>(
                          buildWhen: (previous, current) {
                            return previous.imageWidgetOpen != current.imageWidgetOpen;
                          },
                          listener: (context, state) { },
                          builder: (context, state) {
                            if(state.imageWidgetOpen){
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
                                              children: snapshot.data!,
                                            );
                                        }
                                        return Center(child: CircularImageSpinner(duration: const Duration(seconds: 5),),);
                                      }
                                    );
                                  },
                                )
                              ),
                            );
                            }
                            else{
                              return FloatingActionButton(key: const Key("MenuButton"), onPressed: (){context.read<ImageBloc>().add(GetLocalImages()); context.read<BoardBloc>().add(OpenCloseEvent(true)); }, child: Icon(Icons.menu),);
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

  Future<List<Widget>> _ConstructImages(List<String> images) async{
    List<Widget> list = [];
    for (var i = 0; i < images.length; i++) {
      list.add(_drawerImage(images[i])); 
    }
    return list;
  }

  class _drawerImage extends StatelessWidget{
    _drawerImage(this.image);
    String image;
    @override
    Widget build(BuildContext context) {
      return GestureDetector(
        onTapUp: (details) {
          context.read<BoardBloc>().add(DraggableDropped(const Offset(50, 50), image));
          context.read<BoardBloc>().add(OpenCloseEvent(false));
        },
        child: Image.memory(base64Decode(image)),
      );
    }
  }

  Future<List<Widget>> _boardWidgets(List<DraggableImage> data) async{
    List<Widget> list = [];
    for (var i = 0; i < data.length; i++) {
      Widget pos = Positioned(
        height: 100,
        width: 100,
        top: data[i].offset.dy,
        left: data[i].offset.dx,
        child: Draggable(
          key: const Key("dragtest"),
          child: Image.memory(base64Decode(data[i].image)),
          feedback: Container(height: 100, width: 100, child: Image.memory(base64Decode(data[i].image))),
          data: Image.memory(base64Decode(data[i].image)),
          onDragStarted: () {
            
          },
          onDragEnd: (details) {
            if(details.wasAccepted){
              data[i].offset = details.offset;
            }
          },
        ),
        );
      list.add(pos);
    }
    return list;
  }