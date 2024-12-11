using System.Runtime.CompilerServices;

namespace MiraiAnimation.Model.Services {
	public interface IDbService<T, TProperty> {
		T GetById(string id);
		IEnumerable<T> GetAll();
		bool AddElement(T element);
		bool RemoveElement(T element);
		T EditElement(T element);
		T GetByProperty(TProperty property);
	}
}
