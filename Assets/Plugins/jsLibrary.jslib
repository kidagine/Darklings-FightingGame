mergeInto(LibraryManager.library, {
    FunctionImplementedInJavaScriptLibraryFile: function (utf8String) {
        var jsString = UTF8ToString(utf8String);
        window.alert(jsString);
    },
        LoadData: function(yourkey) 
        {
            if (navigator.userAgent.indexOf("Firefox") != -1)
            {
                return null;
            }
            else
            {
                if (localStorage.getItem(Pointer_stringify(yourkey)) !== null)
                {
                    var returnStr = localStorage.getItem(UTF8ToString(yourkey));
                    var bufferSize = lengthBytesUTF8(returnStr) + 1;
                    var buffer = _malloc(bufferSize);
                    stringToUTF8(returnStr, buffer, bufferSize);
                    return buffer;
                }
            }
        },

        SaveData: function(yourkey, yourdata)
        {
            if (navigator.userAgent.indexOf("Firefox") != -1)
            {
            }
            else
            {                   
                localStorage.setItem(UTF8ToString(yourkey), UTF8ToString(yourdata));
            }
        }
})