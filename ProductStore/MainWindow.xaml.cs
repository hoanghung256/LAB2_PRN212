using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public MainWindow()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        public void LoadCategoryList()
        {
            try
            {
                var catList = categoryRepository.GetCategories();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryID";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error on load list of categories");
            }
        }

        public void LoadProductList()
        {
            try
            {
                var productList = productRepository.GetProducts();
                var product = productList[0];
                dgData.ItemsSource = productList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of products");

            }
            finally
            {
                resetInput();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductName == null || txtPrice == null || txtUnitsInStock == null || cboCategory == null)
                {
                    MessageBox.Show("One or more UI elements are not initialized.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Please enter a product name.");
                    return;
                }

                if (!Decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                if (!short.TryParse(txtUnitsInStock.Text, out short unitsInStock))
                {
                    MessageBox.Show("Please enter a valid number for units in stock.");
                    return;
                }
                Category selectedCate = (Category) cboCategory.SelectedItem;
                if (selectedCate == null)
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = Decimal.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryID = selectedCate.CategoryID;

                productRepository.AddProduct(product);
            }
            catch (Exception)
            {
                MessageBox.Show("Action wrong");
            }
            finally
            {
                LoadProductList();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedIndex == -1) return;
            DataGrid dataGrid = sender as DataGrid;

            if (dataGrid.SelectedIndex < 0) return;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row == null) return;

            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
            if (RowColumn == null) return;

            string id = ((TextBlock) RowColumn.Content)?.Text;

            if (id == null) return;

            Product product = productRepository.GetProductById(Int32.Parse(id));

            if (product == null) return;

            txtProductID.Text = product.ProductID.ToString();
            txtProductName.Text = product.ProductName;
            txtPrice.Text = product.UnitPrice.ToString();
            txtUnitsInStock.Text = product.UnitsInStock.ToString();
            cboCategory.SelectedValue = product.Category.CategoryID;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Category selectedCate = (Category) cboCategory.SelectedItem;
                    Product product = new Product();
                    product.ProductID = Int32.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = Decimal.Parse(txtPrice.Text);
                    product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryID = selectedCate.CategoryID;

                    productRepository.UpdateProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Category selectedCate = (Category) cboCategory.SelectedItem;
                    Product product = new Product();
                    product.ProductID = Int32.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = Decimal.Parse(txtPrice.Text);
                    product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryID = selectedCate.CategoryID;

                    productRepository.DeleteProduct(product);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You must select a Product!");
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetInput()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnitsInStock.Text = "";
        }
    }
}
