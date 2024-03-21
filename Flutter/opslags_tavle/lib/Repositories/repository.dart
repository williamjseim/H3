
abstract class Repository {
  Future<List<String>> getImages(String key);
  Future<void> saveImage(String key, String base64);
}