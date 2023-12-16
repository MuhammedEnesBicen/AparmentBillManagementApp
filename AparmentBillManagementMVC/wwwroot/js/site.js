// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function fireAlert(alertMessage) {
    Swal.fire({
        title: 'Info',
        text: alertMessage,
        confirmButtonColor: '#f20f0f',
        confirmButtonText: 'Okay'
    });
}

function addBillAlert() {
    let message = "You can't add bill for this apartment. Because this apartment doesnt have a tenant.";
    fireAlert(message);
}