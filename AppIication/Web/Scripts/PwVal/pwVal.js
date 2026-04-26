

function showTooltip(thisTt) {
    document.getElementById(thisTt).classList.add("show");
}

function hideTooltip(txtPw,thisTt) {
    const password = document.getElementById(txtPw).value;
    if (password === "") {
        document.getElementById(thisTt).classList.remove("show");
    }
}

function validatePassword(txtPw,passLen,passUpp,passNum,passSpec) {

    const password = document.getElementById(txtPw).value;

    //alert(passLen + "-" + passUpp + "-" + passNum + "-" + passSpec);

    //passwordValid.length = password.length >= 8;
    //passwordValid.uppercase = /[A-Z]/.test(password);
    //passwordValid.number = /[0-9]/.test(password);
    //passwordValid.special = /[^A-Za-z0-9]/.test(password);

    toggle(passLen, password.length >= 8);
    toggle(passUpp, /[A-Z]/.test(password));
    toggle(passNum, /[0-9]/.test(password));
    toggle(passSpec, /[^A-Za-z0-9]/.test(password));

}

function toggle(id, valid) {

    var el = document.getElementById(id);

    if (valid) {
        el.classList.remove("invalid");
        el.classList.add("valid");
    } else {
        el.classList.remove("valid");
        el.classList.add("invalid");
    }

}

