/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 4.0.1
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

package io.adaptivecards.objectmodel;

public class RemoteResourceInformation {
  private transient long swigCPtr;
  private transient boolean swigCMemOwn;

  protected RemoteResourceInformation(long cPtr, boolean cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  protected static long getCPtr(RemoteResourceInformation obj) {
    return (obj == null) ? 0 : obj.swigCPtr;
  }

  protected void swigSetCMemOwn(boolean own) {
    swigCMemOwn = own;
  }

  @SuppressWarnings("deprecation")
  protected void finalize() {
    delete();
  }

  public synchronized void delete() {
    if (swigCPtr != 0) {
      if (swigCMemOwn) {
        swigCMemOwn = false;
        AdaptiveCardObjectModelJNI.delete_RemoteResourceInformation(swigCPtr);
      }
      swigCPtr = 0;
    }
  }

  public void setUrl(String value) {
    AdaptiveCardObjectModelJNI.RemoteResourceInformation_url_set(swigCPtr, this, value);
  }

  public String getUrl() {
    return AdaptiveCardObjectModelJNI.RemoteResourceInformation_url_get(swigCPtr, this);
  }

  public void setMimeType(String value) {
    AdaptiveCardObjectModelJNI.RemoteResourceInformation_mimeType_set(swigCPtr, this, value);
  }

  public String getMimeType() {
    return AdaptiveCardObjectModelJNI.RemoteResourceInformation_mimeType_get(swigCPtr, this);
  }

  public RemoteResourceInformation() {
    this(AdaptiveCardObjectModelJNI.new_RemoteResourceInformation(), true);
  }

}
