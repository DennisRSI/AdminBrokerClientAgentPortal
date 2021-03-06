﻿var PROFILE = new Profile();

function Profile() {
    var self = this;

    this.init = function (id) {

        $(".toggle-accordion").on("click", function () {

            var accordionId = $(this).attr("accordion-id"),
                numPanelOpen = $(accordionId + ' .collapse.in').length;

            $(this).toggleClass("active");

            if (numPanelOpen === 0) {
                self.openAllPanels(accordionId);
            } else {
                self.closeAllPanels(accordionId);
            }
        });

        $("#password-save").on("click", function () {

            var password1 = $('#password1').val();
            var password2 = $('#password2').val();

            if (password1 !== password2) {
                alert('Passwords do not match');
                return;
            }

            var url = '/api/user/changepassword/' + id + '/' + password1;

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                success: function (result) {
                    if (result.isSuccess === true) {
                        $('.modal').modal('hide');
                        alert('Password successfully changed');
                    }
                    else {
                        var alertMessage = "";

                        for (var i = 0; i < result.messages.length; i++) {
                            alertMessage += result.messages[i] + "\n";
                        }

                        alert(alertMessage);
                    }
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            });
        });

        $("#profile-submit").on("click", function (event) {

            var target = $(event.target);
            var form = target.parents('form');
            var valid = form.valid();

            if (!valid) {
                return;
            }

            var data = UTILITY.serializeFormJSON(form);

            $.ajax({
                url: '/api/user/updateprofile/' + id,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.isSuccess === true) {
                        alert('Profile successfully updated.');
                    }
                    else {
                        alert(result.message);
                    }
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            });
        });

        $("#agentdeactivate").on("click", function (event) {
            self.deactivateAgent();
        });
    };

    this.openAllPanels = function (aId) {
        $(aId + ' .panel-collapse:not(".in")').collapse('show');
    };

    this.closeAllPanels = function (aId) {
        $(aId + ' .panel-collapse.in').collapse('hide');
    };

    this.deactivateAgent = function () {
        var agentId = $('#agentdeactivate').data('id');
        var reason = $('#reason').val();
        reason = encodeURIComponent(reason);

        var url = ['/api/user/deactivateagent', agentId, reason].join('/');

        $.post(url,
            function (data) {
                $('.modal').modal('hide');
                $('#deactivate-agent-openmodal').addClass('hidden');
                $('#deactivate-agent-message').show();
            }
        );
    };
}
