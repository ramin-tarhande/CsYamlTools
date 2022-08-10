using System;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace CsYamlTools
{
    public class PolymorphicDeserializerBuilder
    {
        private readonly Deserializer deserializer;

        public PolymorphicDeserializerBuilder()
        {
            deserializer = new Deserializer();
        }

        public void RegisterTag<T>(string tagTitle)
        {
            deserializer.RegisterTagMapping("tag:yaml.org,2002:" + tagTitle, typeof(T));
        }

        public void RegisterDefaultDerivedType<BaseType, DefaultDerivedType>()
        {
            deserializer.TypeResolvers.Add(new DefaultSetterReslover<BaseType, DefaultDerivedType>());
        }

        public Deserializer Product()
        {
            return deserializer;
        }

        class DefaultSetterReslover<BaseType, DefaultDerivedType> : INodeTypeResolver
        {
            bool INodeTypeResolver.Resolve(NodeEvent nodeEvent, ref Type currentType)
            {
                if (currentType == typeof(BaseType))
                {
                    if (nodeEvent.Tag == null)
                        currentType = typeof(DefaultDerivedType);
                    return true;
                }

                return false;
            }
        }
    }
}
