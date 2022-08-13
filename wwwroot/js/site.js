// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    const PlaceHolderElement = $('#placeHolderHere');
    $('button[data-bs-toggle="ajax-modal"]').click(function(e) {
        const url = $(this).data('url');
        const decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function(data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-bs-save="modal"]', function(e) {
        const form = $(this).parents('.modal').find('form');
        const actionUrl = form.attr('action');
        const sendData = form.serialize();
        $.post(actionUrl, sendData).done(function(data) {
            PlaceHolderElement.find('.modal').modal('hide');
            window.location.reload();
        })
    })
})