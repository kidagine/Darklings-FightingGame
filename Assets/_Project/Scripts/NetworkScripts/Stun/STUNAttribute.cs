using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STUN.Attributes;

namespace STUN
{
    public abstract class STUNAttribute 
    {
        static Dictionary<int, Type> _knownAttributes;
        static Dictionary<Type, int> _knownTypes;

        static STUNAttribute()
        {
            _knownAttributes = new Dictionary<int, Type>();
            _knownTypes = new Dictionary<Type, int>();

            AddAttribute<STUNMappedAddressAttribute>(1);
            AddAttribute<STUNResponseAddressAttribute>(2);
            AddAttribute<STUNChangeRequestAttribute>(3);
            AddAttribute<STUNSourceAddressAttribute>(4);
            AddAttribute<STUNChangedAddressAttribute>(5);
            AddAttribute<STUNUsernameAttribute>(6);
            AddAttribute<STUNPasswordAttribute>(7);
            AddAttribute<STUNMessageIntegrityAttribute>(8);
            AddAttribute<STUNErrorCodeAttribute>(9);
            //AddAttribute<>(10);
            AddAttribute<STUNReflectedFromAttribute>(11);
            AddAttribute<STUNXorMappedAddressAttribute>(0x0020);
            AddAttribute<STUNResponseOriginAttribute>(0x802B);
            AddAttribute<STUNOtherAddressAttribute>(0x802C);
        }

        public abstract void Parse(STUNBinaryReader binary, int length);

        public virtual void Write(STUNBinaryWriter binary)
        {
            binary.Write((ushort)GetAttribute(GetType()));
            var lengthPos = binary.BaseStream.Position;
            binary.Write((ushort)0);
            var bodyPos = binary.BaseStream.Position;
            WriteBody(binary);
            var length = binary.BaseStream.Position - bodyPos;
            var endPos = binary.BaseStream.Position;
            binary.BaseStream.Position = lengthPos;
            binary.Write((ushort)length);
            binary.BaseStream.Position = endPos;
        }

        public abstract void WriteBody(STUNBinaryWriter binary);

        public static void AddAttribute<T>(int type) where T : STUNAttribute
        {
            _knownAttributes.Add(type, typeof(T));
            _knownTypes.Add(typeof(T), type);
        }

        public static Type GetAttribute(int attribute)
        {
            Type type;

            if (_knownAttributes.TryGetValue(attribute, out type))
            {
                return type;
            }

            return null;
        }

        public static int GetAttribute(Type type)
        {
            int attr;

            if (_knownTypes.TryGetValue(type, out attr))
            {
                return attr;
            }

            return -1;
        }
    }
}
