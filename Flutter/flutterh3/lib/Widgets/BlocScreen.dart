import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutterh3/Bloc/counterBloc.dart';
import 'package:flutterh3/main.dart';
import 'package:go_router/go_router.dart';

class BlocScreen extends StatelessWidget{
  const BlocScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
    providers: [
      BlocProvider(create: (context) => CounterBloc())
    ],
     child: Scaffold(
      appBar: mainappbar(context),
      body: BlocBuilder<CounterBloc, int>(
        builder: (context, state) => Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              const Text(
                'You have pushed the button this many times:',
              ),
                    Text(
                      '$state',
                      style: Theme.of(context).textTheme.headlineMedium,
                    ),
              Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Container(
                                padding: const EdgeInsets.all(10),
                                child: Theme(
                                  data: ThemeData(
                                    shadowColor: const Color.fromARGB(255, 9, 255, 0)
                                  ),
                                  child: BlocConsumer<CounterBloc, int>(
                                    listener: (context, state) { },
                                    builder: (context, state) => Visibility(
                                        visible: !(state >= 10),
                                        child: FloatingActionButton(
                                          heroTag: "button1",
                                          backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                                          onPressed: () { context.read<CounterBloc>().add(CounterIncrementEvent()); },
                                          tooltip: 'Increment',
                                          child: const Icon(Icons.add, color: Color.fromARGB(255, 9, 255, 0),),
                                        ),
                                      ),
                                  ),
                                  ),
                                  ),
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: Theme(
                                  data: ThemeData(
                                    shadowColor: const Color.fromARGB(255, 9, 255, 0)
                                  ),
                                  child: Visibility(
                                    visible: !(state <= 0),
                                    child: FloatingActionButton(
                                      heroTag: "button2",
                                      backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                                      onPressed: () { context.read<CounterBloc>().add(CounterDecrementEvent()); },
                                      tooltip: 'Decrement',
                                      child: const Icon(Icons.remove, color: Color.fromARGB(255, 9, 255, 0),),
                                    ),
                                  ),
                                  ),
                                ), 
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: Theme(
                                  data: ThemeData(
                                    shadowColor: const Color.fromARGB(255, 9, 255, 0)
                                  ),
                                  child: Visibility(
                                    visible: !(state <= 0),
                                    maintainState: true,
                                    replacement: FloatingActionButton(
                                      heroTag: "button2",
                                      backgroundColor: Color.fromARGB(255, 51, 51, 51),
                                      onPressed: () { context.read<CounterBloc>().add(CounterDecrementEvent()); },
                                      tooltip: 'Decrement',
                                      child: const Icon(Icons.remove, color: Color.fromARGB(255, 2, 71, 0),),
                                    ),
                                    child: FloatingActionButton(
                                      heroTag: "button2",
                                      backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                                      onPressed: () { context.read<CounterBloc>().add(CounterDecrementEvent()); },
                                      tooltip: 'Decrement',
                                      child: const Icon(Icons.remove, color: Color.fromARGB(255, 9, 255, 0),),
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