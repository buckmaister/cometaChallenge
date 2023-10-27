function authenticate() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    if (password === "torino") {
        alert("Autenticazione riuscita. Accesso consentito.");
        localStorage.setItem("username", username);
        window.location.href = '/unityBuild/index.html';
    } else {
        alert("Autenticazione fallita. Nome utente o password errati.");
    }
}
