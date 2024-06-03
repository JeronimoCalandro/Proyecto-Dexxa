var OpenWindowPlugin = {
    openWindow: function(link)
    {
        window.location.href = UTF8ToString(link);
    }
};

mergeInto(LibraryManager.library, OpenWindowPlugin);