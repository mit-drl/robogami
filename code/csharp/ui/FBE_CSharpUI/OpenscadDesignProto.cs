//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: openscad.proto
namespace FabByExample.proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OpenscadParameter")]
  public partial class OpenscadParameter : global::ProtoBuf.IExtensible
  {
    public OpenscadParameter() {}
    
    private string _name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private double _initial_value;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"initial_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public double initial_value
    {
      get { return _initial_value; }
      set { _initial_value = value; }
    }
    private double _min = default(double);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"min", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(double))]
    public double min
    {
      get { return _min; }
      set { _min = value; }
    }
    private double _max = default(double);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"max", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(double))]
    public double max
    {
      get { return _max; }
      set { _max = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OpenscadDesign")]
  public partial class OpenscadDesign : global::ProtoBuf.IExtensible
  {
    public OpenscadDesign() {}
    
    private readonly global::System.Collections.Generic.List<FabByExample.proto.OpenscadParameter> _parameter = new global::System.Collections.Generic.List<FabByExample.proto.OpenscadParameter>();
    [global::ProtoBuf.ProtoMember(1, Name=@"parameter", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<FabByExample.proto.OpenscadParameter> parameter
    {
      get { return _parameter; }
    }
  
    private string _code;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"code", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string code
    {
      get { return _code; }
      set { _code = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}