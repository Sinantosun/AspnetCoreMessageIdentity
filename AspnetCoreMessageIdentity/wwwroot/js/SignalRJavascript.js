$(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7071/SignalRHub").build();
    connection.start().then(() => {
        connection.invoke("GetNickName", "test");
        connection.invoke("SendMessageAsync", "kadir@gmail.com");
    }).catch((err) => { console.log(err); });

    connection.on("clientJoined", data => {
        const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3500,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.onmouseenter = Swal.stopTimer;
                toast.onmouseleave = Swal.resumeTimer;
            }
        });
        Toast.fire({
            icon: "info",
            title: data + " oturum açtı",
        });

    });

    $("#testbtn").click(function () {
        alert(1);
    });

    connection.on("reciveMessage", data => {
        Toast.fire({
            icon: "info",
            title: "Yeni Mesajınız Var!",
        });
    });


});