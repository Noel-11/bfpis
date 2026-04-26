
  

function clickEnterSearch(ctrl) {
    if (event.keyCode === 13) {
        //event.preventDefault();
        //alert(ctrl + "asdad");
        //document.getElementById(ctrl).click();
        var button = document.getElementById(ctrl);

        button.click();

        return false
    }
};
