using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.DAL;
using Epam.ExtPosterStore.Dao.Interfaces;

namespace Epam.ExtPosterStore.BLL.Ioc
{
    public static class DependencyResolver
    {
        private static IProductDao _productDao;
        public static IProductDao ProductDao => _productDao ?? (_productDao = new ProductDao());


        private static IProductLogic _productLogic;
        public static IProductLogic ProductLogic => 
            _productLogic ?? (_productLogic = new ProductLogic(ProductDao,TypeOfProductDao,CategoryDao,ProductOrderDao));



        private static ICategoryDao _categoryDao;
        public static ICategoryDao CategoryDao =>
            _categoryDao ?? (_categoryDao = new CategoryDao());

        private static ICategoryLogic _categoryLogic;

        public static ICategoryLogic CategoryLogic =>
            _categoryLogic ?? (_categoryLogic = new CategoryLogic(CategoryDao,ProductDao));



        private static ITypeOfProductDao _typeOfProductDao;
        public static ITypeOfProductDao TypeOfProductDao =>
            _typeOfProductDao ?? (_typeOfProductDao = new TypeOfProductDao());


        private static ITypeOfProductLogic _typeOfProductLogic;
        public static ITypeOfProductLogic TypeOfProductLogic =>
            _typeOfProductLogic ?? (_typeOfProductLogic = new TypeOfProductLogic(TypeOfProductDao,ProductDao));


        private static IOrderStateDao _orderStateDao;
        public static IOrderStateDao OrderStateDao =>
            _orderStateDao ?? (_orderStateDao = new OrderStateDao());

        private static IOrderStateLogic _orderStateLogic;

        public static IOrderStateLogic OrderStateLogic =>
            _orderStateLogic ?? (_orderStateLogic = new OrderStateLogic(OrderStateDao));




        private static IRoleDao _roleDao;
        public static IRoleDao RoleDao =>
            _roleDao ?? (_roleDao = new RoleDao());

        private static IRoleLogic _roleLogic;
        public static IRoleLogic RoleLogic =>
            _roleLogic ?? (_roleLogic = new RoleLogic(RoleDao));


        private static IUserDao _userDao;
        public static IUserDao UserDao =>
            _userDao ?? (_userDao = new UserDao());

        private static IUserLogic _userLogic;

        public static IUserLogic UserLogic =>
            _userLogic ?? (_userLogic = new UserLogic(UserDao,RoleDao));

        //private static ICartDao _cartDao = new CartDao();

        public static ICartDao CartDao => new CartDao();
            //_cartDao ?? (_cartDao = new CartDao());

        //private static ICartLogic _cartLogic = new CartLogic(_cartDao,ProductDao);

        public static ICartLogic CartLogic => new CartLogic(CartDao,ProductDao);
        //_cartLogic ?? (_cartLogic = new CartLogic(CartDao));


        private static IPayDao _payDao;

        public static IPayDao PayDao =>
            _payDao ?? (_payDao = new PayDao());




        private static IOrderDao _orderDao;
        public static IOrderDao OrderDao =>
            _orderDao ?? (_orderDao = new OrderDao());

        private static IOrderLogic _orderLogic;

        public static IOrderLogic OrderLogic =>
            _orderLogic ?? (_orderLogic = new OrderLogic(OrderDao,OrderStateDao,PayDao,UserDao));


        private static IProductOrderDao _productOrderDao;
        public static IProductOrderDao ProductOrderDao =>
            _productOrderDao ?? (_productOrderDao = new ProductOrderDao());

        private static IProductOrderLogic _productOrderLogic;

        public static IProductOrderLogic ProductOrderLogic =>
            _productOrderLogic ?? (_productOrderLogic = new ProductOrderLogic(ProductOrderDao));

    }
}
