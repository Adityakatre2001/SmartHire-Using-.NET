import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from '../components/commons/Navbar/Navbar';
import Home from '../components/commons/Home/Home';
import About from '../components/pages/Home/js/About';
import Contact from '../components/pages/Home/js/Contact';
import SignIn from '../components/pages/Home/js/SignIn';
import Register from '../components/pages/Home/js/Register';
import ProtectedRoute from './ProtectedRoute'; // Corrected import
import AllUsers from '../components/pages/Admin/UserManagement/AllUsers'; // Example import, ensure all are correct
import UserDetail from '../components/pages/Admin/UserManagement/UserDetails';
import CreateUser from '../components/pages/Admin/UserManagement/CreateUser';
import UpdateUser from '../components/pages/Admin/UserManagement/UpdateUser';
import AllCompanies from '../components/pages/Admin/CompanyManagement/AllCompanies';
import CompanyDetails from '../components/pages/Admin/CompanyManagement/CompanyDetails';
import CreateCompany from '../components/pages/Admin/CompanyManagement/CreateCompany';
import UpdateCompany from '../components/pages/Admin/CompanyManagement/UpdateCompany';

function AppRouter() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/about" element={<About />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/signin" element={<SignIn />} />
        <Route path="/register" element={<Register />} />


        <Route 
          path="/admin/*" 
          element={
            <ProtectedRoute>
              <Routes>
                <Route path="users" element={<AllUsers />} />
                <Route path="users/:id" element={<UserDetail />} />
                <Route path="users/create" element={<CreateUser />} />
                <Route path="users/update/:id" element={<UpdateUser />} />
                <Route path="companies" element={<AllCompanies />} />
                <Route path="companies/:id" element={<CompanyDetails />} />
                <Route path="companies/create" element={<CreateCompany />} />
                <Route path="companies/update/:id" element={<UpdateCompany />} />
              </Routes>
            </ProtectedRoute>
          }
        />
      </Routes>
    </Router>
  );
}

export default AppRouter;
