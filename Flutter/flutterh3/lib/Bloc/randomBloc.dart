import 'dart:math';

import 'package:flutter_bloc/flutter_bloc.dart';

sealed class RandomEvent {}

final class RandomIncrement extends RandomEvent {}

final class RandomDecrement extends RandomEvent {}

final class CreateRandom extends RandomEvent {}

class RandomBloc extends Bloc<RandomEvent, RandomObject> {
  RandomBloc() : super(RandomObject(0, 0)) {
    on<RandomIncrement>((event, emit) =>
        emit(RandomObject(state._maxNumber, state._number + 1)));
    on<RandomDecrement>((event, emit) => emit(RandomObject(
        state.maxNumber, (state._number == 0) ? 0 : state.number - 1)));
    on<CreateRandom>((event, emit) =>
        emit(RandomObject(Random().nextInt(100), state.number)));
  }

  @override
  void onChange(Change<RandomObject> change) {
    super.onChange(change);
  }
}

class RandomObject {
  RandomObject(this._maxNumber, this._number);
  int _number;
  int get number => _number;

  int _maxNumber;
  int get maxNumber => _maxNumber;
}
