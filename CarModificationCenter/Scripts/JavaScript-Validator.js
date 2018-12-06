function validateSize() {
    var file = document.getElementById("file1");    
    if (file.files[0].size > 4194304) { //check file greater than 4MB
        document.getElementById("file1").value = null;
        alert("Warning selected file larger than 4MB");
        $("#file1-alert").html("Selected file larger than 4MB, Select new file!!");
        return false;
    }
    else {
        return true;
    }
}