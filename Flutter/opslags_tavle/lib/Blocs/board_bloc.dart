import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

sealed class BoardEvent {}

class DraggableDropped extends BoardEvent {
  DraggableDropped(this.offset, this.image);
  Offset offset;
  String image;
}

class MoveDroppedEvent extends BoardEvent{
  MoveDroppedEvent(this.offset);
  Offset offset;
}

class OpenCloseEvent extends BoardEvent { OpenCloseEvent(this.open); bool open;}
class ClearEvent extends BoardEvent {}

class BoardBloc extends Bloc<BoardEvent, BoardData>{
  BoardBloc(super.initialState){
    on<OpenCloseEvent>((event, emit) { emit(BoardData.Fill(state.widgets, event.open)); });
    on<DraggableDropped>((event, emit) {
      var data = DraggableImage(event.offset, event.image);
      var newState = BoardData.Fill(state.widgets, !state.imageWidgetOpen);
      newState.widgets.add(data);
      newState.boardChanged = true;
      emit(newState);
    });
  }
}

class BoardData{
  BoardData();
  BoardData.Fill(this.widgets, this.imageWidgetOpen);
  List<DraggableImage> widgets = [];
  bool imageWidgetOpen = false;
  bool boardChanged = false;
}

class DraggableImage{
  DraggableImage(this.offset, this.image);
  Offset offset;
  String image;
}