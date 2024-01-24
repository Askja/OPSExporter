using LargeXlsx;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace OPSExporter.Xlsx;

public class Writer(string fileName) {
    private XlsxWriter _xlsxWriter = null!;
    private bool _isOpened;

    public void Init() {
        try {
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            _xlsxWriter = new XlsxWriter(stream);

            _isOpened = true;
        } catch (Exception) {
            _isOpened = false;
        }
    }

    public bool IsOpened() {
        return _isOpened;
    }

    public Writer Row() {
        if (IsOpened()) {
            _xlsxWriter.BeginRow();
        }

        return this;
    }
    
    public Writer Sheet(string name) {
        if (IsOpened()) {
            _xlsxWriter.BeginWorksheet(name);
        }

        return this;
    }
    
    public Writer Write(string? data) {
        _xlsxWriter.Write(data, XlsxStyle.Default.With(XlsxNumberFormat.Text));

        return this;
    }
    
    public Writer Write(int data) {
        _xlsxWriter.Write(data, XlsxStyle.Default.With(XlsxNumberFormat.Integer));

        return this;
    }
    
    public Writer Write(double data) {
        _xlsxWriter.Write(data, XlsxStyle.Default.With(XlsxNumberFormat.Integer));

        return this;
    }
    
    public Writer Write(long data) {
        _xlsxWriter.Write((decimal)data, XlsxStyle.Default.With(XlsxNumberFormat.Integer));

        return this;
    }
    
    public Writer Write(DateTime data) {
        _xlsxWriter.Write(data, XlsxStyle.Default.With(XlsxNumberFormat.ShortDate));

        return this;
    }

    ~Writer() {
        _xlsxWriter.Dispose();
    }
}