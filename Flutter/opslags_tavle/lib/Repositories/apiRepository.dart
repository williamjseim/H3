import 'dart:isolate';

import 'package:opslags_tavle/Repositories/repository.dart';
import 'package:universal_io/io.dart';

class ApiRepository extends Repository{
  @override
  Future<List<String>> getImages(String key) async {
    Future<void> _isolateUpload() async{
    HttpClient client = HttpClient();
    Uri uri;
    if(Platform.isAndroid){
      uri = Uri.http("10.0.2.2:5142", "/Image", {"Key" : key});
    }
    else{
      uri = Uri.http("localhost:5142", "/Image", {"Key" : key});
    }
      var request = await client.getUrl(uri);
      var response = await request.close();
    }
    return [];
  }

  @override
  Future<void> saveImage(String key, String base64) async {
    Isolate.spawn((message) {_isolateUpload(message);}, base64);
  }

  Future<void> _isolateUpload(String base64) async{
  HttpClient client = HttpClient();
  Uri uri;
  if(Platform.isAndroid){
    uri = Uri.http("10.0.2.2:5142", "/Image", {"Image" : base64});  
  }
  else{
    uri = Uri.http("localhost:5142", "/Image", {"Image" : base64});  
  }

    var request = await client.postUrl(uri);
    var response = await request.close();
  }
}