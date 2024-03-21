import 'dart:convert';

import 'package:flutter/foundation.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:opslags_tavle/Repositories/repository.dart';

class SecureLocalStorageRepository extends Repository{
  @override
  Future<List<String>> getImages(String key) async {
    FlutterSecureStorage storage = const FlutterSecureStorage();
    List<String> images;
    if(await storage.containsKey(key: key)){
      var list = await storage.read(key: key);
      images = await compute<String, List<String>>((message) => List<String>.from(json.decode(message) as List<dynamic>), list!);
    }
    else{
      images = [];
    }
    return images;
  }
  
  @override
  Future<void> saveImage(String key, String base64) async {
    FlutterSecureStorage storage = const FlutterSecureStorage();
    List<String> images = await getImages(key);
    images.add(base64);
    String jsonString = await compute<List<String>, String>((message) => json.encode(message), images);
    await storage.write(key: key, value: jsonString);
  }

}