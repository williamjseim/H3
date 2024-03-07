import 'package:flutter_bloc/flutter_bloc.dart';

sealed class CounterEvent {}

final class CounterIncrementEvent extends CounterEvent {}
final class CounterDecrementEvent extends CounterEvent {}



class CounterBloc extends Bloc<CounterEvent, int>{
  CounterBloc() : super(0){
    on<CounterIncrementEvent>((event, emit) => emit(state + 1));
    on<CounterDecrementEvent>((event, emit) => emit((state == 0) ? 0 : state -1));

  }

  @override
  void onChange(Change<int> change) {
    super.onChange(change);
  }
}