
$(document).on('change', '.img-preview-tracker', function () {
    console.log("ChangeVal")
    console.log(this);
    ImagePreview(this);
})
Number.prototype.toTurkish = function () {
    return this.toLocaleString("tr-TR", { 'minimumFractionDigits': 2, 'maximumFractionDigits': 2 });
}
String.prototype.toTurkishLira = function () {
    return this + " ₺";
}
String.prototype.log = function () {
    if (window.debugMode) {
        return this;
    }
    return;
}
function Log(object) {
    if (window.debugMode) {
        console.log(object);
    }
    return;
}
function DataTable(tableId, url, type, columnList, data,) {
    console.log("tableid" + tableId)
    var table = $("#" + tableId).DataTable({
        "language": {
            "url": "/Backoffice/Lib/DataTables/Turkish.json"
        },

        ajax: {
            url: url,
            type: type,
            data: data,
            dataSrc: function (json) {
                console.log(json.data)
                if (json.status === true) {
                    toastr["success"](json.message, "Başarılı.")
                }
                else if (json.status === false) {
                    toastr["error"](json.message, "İç sunucu isteği işleyemedi.")


                }
                console.log(json);
                return json.data;
            },
        },
        columns: columnList,
        "responsive": true,
        "lengthChange": false,
        "buttons": [
            {
                className: "bg-primary",
                text: "<i class='fas fa-plus'></i><span style='margin-left: 5px'>Yeni</span>",
                action: function (e, dt, node, config) {
                    OpenNewRecordModal();
                }
            },
            { extend: 'copy', text: 'Kopyala', className: 'bg-primary' },
            { extend: 'excel', text: 'Excel İndir', className: 'bg-primary', exportOptions: { columns: ':visible' } },
            { extend: 'pdf', text: 'PDF İndir', className: 'bg-primary', exportOptions: { columns: ':visible' } },
            { extend: 'print', text: 'Yazdır', className: 'bg-primary', exportOptions: { columns: ':visible' } },
        ],
        initComplete: function () {
            setTimeout(function () {
                table.buttons().container().appendTo('#' + tableId + '_wrapper .col-md-6:eq(0)');
            }, 10);
        }
    })
    console.log($('#' + tableId + '_wrapper.col-md-6:eq(0)'))


    return table;
}
function LoadPage(url, type, location, succesFunc, data) {
    $.ajax({
        url: url,
        type: type,
        data: data,
        success: function (response) {
            console.log(succesFunc)
            if (succesFunc != undefined && succesFunc != null) {
                console.log("run");
                succesFunc();
            }
            $(location).html(response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            toastr["error"]("Görünüm Oluştururken bir hata oluştu. İç sunucuda görüntü oluşturulamadı.", "Sunucu Hatası !")
        }
    });
}
function SelectInput(Input, url, type) {
    if ($(Input).attr('selected-val') != undefined) {
        $.ajax({
            url: url,
            type: type,
            data: { key: '', id: $(Input).attr('selected-val') },
            success: function (response) {
                $(Input).append('<option value="' + response.data[0].id + '">' + response.data[0].text + '</option>');
                $(Input).select2({
                    theme: "bootstrap4",
                    language: "tr",
                    ajax: {
                        url: url,
                        type: type,
                        data: function (params) {
                            var query = {
                                key: params.term,
                            }
                            // Query parameters will be ?search=[term]&type=public
                            return query;
                        },
                        processResults: function (data) {
                            return {
                                results: data.data
                            };
                        }
                    }
                });
            }
        })
    }
    else {
        $(Input).select2({
            theme: "bootstrap4",
            language: "tr",
            ajax: {
                url: url,
                type: type,
                data: function (params) {
                    var query = {
                        key: params.term,
                    }
                    // Query parameters will be ?search=[term]&type=public
                    return query;
                },
                processResults: function (data) {
                    return {
                        results: data.data
                    };
                }
            }
        });
    }


}