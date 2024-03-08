import 'dart:math';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutterh3/Bloc/randomBloc.dart';
import 'package:flutterh3/main.dart';
import 'package:go_router/go_router.dart';

class RandomScreen extends StatelessWidget {
  const RandomScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [BlocProvider(create: (context) => RandomBloc())],
      child: Scaffold(
        appBar: mainappbar(context),
        body: BlocBuilder<RandomBloc, RandomObject>(
          builder: (context, state) => Center(
            child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  const Center(
                    child: Text('You have pushed the button this many times:', ),
                  ),
                  Center(
                    child: Text(
                      state.number.toString(),
                      style: Theme.of(context).textTheme.headlineMedium,
                    ),
                  ),
                  Center(
                    child: Text(
                      state.maxNumber.toString(),
                      style: Theme.of(context).textTheme.headlineMedium,
                    ),
                  ),
                  Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: BlocConsumer<RandomBloc, RandomObject>(
                                  listener: (context, state) { },
                                  builder: (context, state) => Visibility(
                                    visible: !(state.number >= state.maxNumber),
                                    replacement: FloatingActionButton(
                                    hoverColor: const Color.fromARGB(255, 51, 51, 51),
                                    focusColor: const Color.fromARGB(255, 51, 51, 51),
                                    heroTag: "button2",
                                    backgroundColor: Color.fromARGB(255, 51, 51, 51),
                                    onPressed: () { },
                                    tooltip: 'Disabled',
                                    child: const Icon(Icons.add, color: Color.fromARGB(255, 2, 71, 0),),
                                  ),
                                    child: Theme(
                                      data: ThemeData(shadowColor: const Color.fromARGB(255, 9, 255, 0)),
                                      child: FloatingActionButton(
                                        heroTag: "button1",
                                        backgroundColor:
                                            const Color.fromARGB(255, 0, 0, 0),
                                        onPressed: () {
                                          context
                                              .read<RandomBloc>()
                                              .add(RandomIncrement());
                                        },
                                        tooltip: 'Increment',
                                        child: const Icon(
                                          Icons.add,
                                          color: Color.fromARGB(255, 9, 255, 0),
                                        ),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: BlocConsumer<RandomBloc, RandomObject>(
                                  listener: (context, state) { },
                                  builder: (context, state) => Visibility(
                                    visible: !(state.number <= 0),
                                    replacement: FloatingActionButton(
                                    hoverColor: const Color.fromARGB(255, 51, 51, 51),
                                    focusColor: const Color.fromARGB(255, 51, 51, 51),
                                    heroTag: "button2",
                                    backgroundColor: const Color.fromARGB(255, 51, 51, 51),
                                    onPressed: () { },
                                    tooltip: 'Disabled',
                                    child: const Icon(Icons.remove, color: Color.fromARGB(255, 2, 71, 0),),
                                  ),
                                    child: Theme(
                                      data: ThemeData(shadowColor: const Color.fromARGB(255, 9, 255, 0)),
                                      child: FloatingActionButton(
                                        heroTag: "button2",
                                        backgroundColor:
                                            const Color.fromARGB(255, 0, 0, 0),
                                        onPressed: () {
                                          context
                                              .read<RandomBloc>()
                                              .add(RandomDecrement());
                                        },
                                        tooltip: 'Decrement',
                                        child: const Icon(
                                          Icons.remove,
                                          color: Color.fromARGB(255, 9, 255, 0),
                                        ),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: Theme(
                                  data: ThemeData(
                                      shadowColor:
                                          const Color.fromARGB(255, 9, 255, 0)),
                                  child: Visibility(
                                    visible: state.maxNumber == 0,
                                    child: FloatingActionButton(
                                      heroTag: "button2",
                                      backgroundColor:
                                          const Color.fromARGB(255, 0, 0, 0),
                                      onPressed: () {
                                        context
                                            .read<RandomBloc>()
                                            .add(CreateRandom());
                                      },
                                      tooltip: 'Make random',
                                      child: const Icon(
                                        Icons.lunch_dining,
                                        color: Color.fromARGB(255, 9, 255, 0),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                            ]),
                      ]),
                ]),
          ),
        ),
      ),
    );
  }
}
