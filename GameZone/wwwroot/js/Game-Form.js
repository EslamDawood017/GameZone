document.getElementById('fileInput').addEventListener('change', function (event) {
    var file = event.target.files[0];
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var previewDiv = document.getElementById('preview');
            previewDiv.innerHTML = '<img src="' + e.target.result + '" alt="Image Preview" class="Preview">';

        }
        reader.readAsDataURL(file);
    }
});
