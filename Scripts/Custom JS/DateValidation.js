document.addEventListener("DOMContentLoaded", function () {
    var dateOfBirth = document.getElementById("DateOfBirth");

    // Calculate minimum date (Exactly 18 years ago)
    var minDate = new Date();
    minDate.setFullYear(minDate.getFullYear() - 18);
    var dd = String(minDate.getDate()).padStart(2, '0');
    var mm = String(minDate.getMonth() + 1).padStart(2, '0'); // January is 0!
    var yyyy = minDate.getFullYear();
    var minDateFormatted = yyyy + '-' + mm + '-' + dd;

    // Calculate maximum date (Today's date)
    var today = new Date();
    var maxDate = today.toISOString().split('T')[0];

    // Set attributes for date input
    dateOfBirth.setAttribute('max', minDateFormatted); // Set maximum date to exactly 18 years ago
    dateOfBirth.setAttribute('min', '1900-01-01'); 
});
