using FluxStore.Domain.Common;

namespace FluxStore.Domain.Entities
{
	public class Role : BaseEntity
    {
		public string RoleName { get; set; } = "Customer";
	}
}

