this.dragAndDrop = document.querySelector('.drag-and-drop');
this.uploadBtn = document.getElementById('upload-btn');
this.uploadExplorer = document.getElementById('upload-file');
this.files;

this.uploadBtn.addEventListener('click', () => {
    this.uploadExplorer.click();
    console.log('Сlick on file explorer');
});
this.uploadExplorer.onchange = ((e) => {
    console.log('File has started loading to server');

    this.files = e.target.files;
});

this.dragAndDrop.addEventListener('dragenter', (e) => {
    e.preventDefault();
    this.dragAndDrop.classList.add('active')
    console.log('dragenter');
})
this.dragAndDrop.addEventListener('dragleave', (e) => {
    e.preventDefault();
    this.dragAndDrop.classList.remove('active')
    console.log('dragleave');
})
this.dragAndDrop.addEventListener('dragover', (e) => {
    e.preventDefault();
    console.log('dragover');
})

this.dragAndDrop.addEventListener('drop', (e) => {
    e.preventDefault();
    this.dragAndDrop.classList.remove('active')

    this.files = e.dataTransfer.files;
})

function readFile(file){
    return function () {
        console.log(file);
    }
}

function sendContract(){
    var formdata = new FormData(document.forms.addfiles);
    var i = 0, len = this.files.length, file;
    for (; i < len; i++) {
        file = this.files[i];
        formdata.append("uploads", file);
    }
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Home/AddFile");
    xhr.send(formdata);
}