//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace HoloToolkit.Sharing {

public class NetworkOutMessage : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal NetworkOutMessage(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(NetworkOutMessage obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~NetworkOutMessage() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          SharingClientPINVOKE.delete_NetworkOutMessage(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public virtual void Write(byte value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_0(swigCPtr, value);
  }

  public virtual void Write(short value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_1(swigCPtr, value);
  }

  public virtual void Write(int value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_2(swigCPtr, value);
  }

  public virtual void Write(long value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_3(swigCPtr, value);
  }

  public virtual void Write(float value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_4(swigCPtr, value);
  }

  public virtual void Write(double value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_5(swigCPtr, value);
  }

  public virtual void Write(XString value) {
    SharingClientPINVOKE.NetworkOutMessage_Write__SWIG_6(swigCPtr, XString.getCPtr(value));
  }

  public virtual void WriteArray(byte[] data, uint length) {
    global::System.Runtime.InteropServices.GCHandle pinHandle_data = global::System.Runtime.InteropServices.GCHandle.Alloc(data, global::System.Runtime.InteropServices.GCHandleType.Pinned); try {
    {
      SharingClientPINVOKE.NetworkOutMessage_WriteArray(swigCPtr, (global::System.IntPtr)pinHandle_data.AddrOfPinnedObject(), length);
    }
    } finally { pinHandle_data.Free(); }
  }

  public virtual void Reset() {
    SharingClientPINVOKE.NetworkOutMessage_Reset(swigCPtr);
  }

}

}
