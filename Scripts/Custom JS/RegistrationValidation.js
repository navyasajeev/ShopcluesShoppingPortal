$(document).ready(function () {
    $('#submitBtn').click(function (event) {
        var isValid = true;

        // Validate First Name
        var firstName = $('#FirstName').val().trim();
        if (firstName === '' || firstName.length < 3 || firstName.length > 20) {
            $('#firstname-error').text('First name must be between 3 and 20 characters.');
            isValid = false;
        } else {
            $('#firstname-error').text('');
        }

        // Validate Last Name
        var lastName = $('#LastName').val().trim();
        if (lastName === '') {
            $('#lastname-error').text('Last name is required.');
            isValid = false;
        } else {
            $('#lastname-error').text('');
        }

        // Validate Gender (optional)
        // Assuming Gender is validated via radio buttons (M, F, O)
        var genderSelected = $('input[name="Gender"]:checked').length;
        if (genderSelected === 0) {
            $('#gender-error').text('Gender is required.');
            isValid = false;
        } else {
            $('#gender-error').text('');
        }

        // Validate Date of Birth
        var dateOfBirth = $('#DateOfBirth').val().trim();
        if (dateOfBirth === '') {
            $('#dateofbirth-error').text('Date of birth is required.');
            isValid = false;
        } else {
            $('#dateofbirth-error').text('');
        }

        // Validate Phone Number
        var phoneNumber = $('#PhoneNumber').val().trim();
        if (phoneNumber === '' || !/^\d{10}$/.test(phoneNumber)) {
            $('#phonenumber-error').text('Phone number must be 10 digits.');
            isValid = false;
        } else {
            $('#phonenumber-error').text('');
        }

        // Validate Address
        var address = $('#Address').val().trim();
        if (address === '') {
            $('#address-error').text('Address is required.');
            isValid = false;
        } else {
            $('#address-error').text('');
        }

        // Validate State
        var state = $('#State').val();
        if (!state || state === '') {
            $('#state-error').text('State is required.');
            isValid = false;
        } else {
            $('#state-error').text('');
        }

        // Validate City
        var city = $('#City').val();
        if (!city || city === '') {
            $('#city-error').text('City is required.');
            isValid = false;
        } else {
            $('#city-error').text('');
        }

        // Validate Email Address
        var emailAddress = $('#EmailAddress').val().trim();
        if (emailAddress === '' || !/^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$/.test(emailAddress)) {
            $('#emailaddress-error').text('Enter a valid email address.');
            isValid = false;
        } else {
            $('#emailaddress-error').text('');
        }

        // Validate Password
        var password = $('#Password').val().trim();
        if (password === '' || password.length < 6) {
            $('#password-error').text('Password must be at least 6 characters long.');
            isValid = false;
        } else {
            $('#password-error').text('');
        }

        // Validate Confirm Password
        var confirmPassword = $('#ConfirmPassword').val().trim();
        if (confirmPassword === '') {
            $('#confirmpassword-error').text('Confirm password is required.');
            isValid = false;
        } else {
            $('#confirmpassword-error').text('');
        }

        // Validate Password Matching
        if (password !== '' && confirmPassword !== '' && password !== confirmPassword) {
            $('#confirmpassword-error').text('Passwords do not match.');
            isValid = false;
        } else if (password === confirmPassword) {
            $('#confirmpassword-error').text('');
        }

        // Prevent form submission if there are validation errors
        if (!isValid) {
            event.preventDefault();
        }
    });

    // Blur event handlers for individual fields

    // First Name
    $('#FirstName').blur(function () {
        var firstName = $(this).val().trim();
        if (firstName === '') {
            $('#firstname-error').text('First name is required.');
        } else if (firstName.length < 3 || firstName.length > 20) {
            $('#firstname-error').text('First name must be between 3 and 20 characters.');
        } else {
            $('#firstname-error').text('');
        }
    });

    // Last Name
    $('#LastName').blur(function () {
        var lastName = $(this).val().trim();
        if (lastName === '') {
            $('#lastname-error').text('Last name is required.');
        } else {
            $('#lastname-error').text('');
        }
    });

    // Gender (optional, assuming radio buttons)
    $('input[name="Gender"]').change(function () {
        var genderSelected = $('input[name="Gender"]:checked').length;
        if (genderSelected === 0) {
            $('#gender-error').text('Gender is required.');
        } else {
            $('#gender-error').text('');
        }
    });

    // Date of Birth
    $('#DateOfBirth').blur(function () {
        var dateOfBirth = $(this).val().trim();
        if (dateOfBirth === '') {
            $('#dateofbirth-error').text('Date of birth is required.');
        } else {
            $('#dateofbirth-error').text('');
        }
    });

    // Phone Number
    $('#PhoneNumber').blur(function () {
        var phoneNumber = $(this).val().trim();
        if (phoneNumber === '') {
            $('#phonenumber-error').text('Phone number is required.');
        } else if (!/^\d{10}$/.test(phoneNumber)) {
            $('#phonenumber-error').text('Phone number must be 10 digits.');
        } else {
            $('#phonenumber-error').text('');
        }
    });

    // Address
    $('#Address').blur(function () {
        var address = $(this).val().trim();
        if (address === '') {
            $('#address-error').text('Address is required.');
        } else {
            $('#address-error').text('');
        }
    });

    // State
    $('#State').change(function () {
        var state = $(this).val();
        if (!state || state === '') {
            $('#state-error').text('State is required.');
        } else {
            $('#state-error').text('');
        }
    });

    // City
    $('#City').change(function () {
        var city = $(this).val();
        if (!city || city === '') {
            $('#city-error').text('City is required.');
        } else {
            $('#city-error').text('');
        }
    });

    // Email Address
    $('#EmailAddress').blur(function () {
        var emailAddress = $(this).val().trim();
        if (emailAddress === '') {
            $('#emailaddress-error').text('Email address is required.');
        } else if (!/^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$/.test(emailAddress)) {
            $('#emailaddress-error').text('Enter a valid email address.');
        } else {
            $('#emailaddress-error').text('');
        }
    });

    // Password
    $('#Password').blur(function () {
        var password = $(this).val().trim();
        if (password === '') {
            $('#password-error').text('Password is required.');
        } else if (password.length < 6) {
            $('#password-error').text('Password must be at least 6 characters long.');
        } else {
            $('#password-error').text('');
        }
    });

    // Confirm Password
    $('#ConfirmPassword').blur(function () {
        var confirmPassword = $(this).val().trim();
        if (confirmPassword === '') {
            $('#confirmpassword-error').text('Confirm password is required.');
        } else if ($('#Password').val().trim() !== confirmPassword) {
            $('#confirmpassword-error').text('Passwords do not match.');
        } else {
            $('#confirmpassword-error').text('');
        }
    });
});
