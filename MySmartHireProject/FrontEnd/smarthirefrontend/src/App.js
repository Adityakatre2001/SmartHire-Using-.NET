import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/commons/Navbar/Navbar';
import Home from './components/commons/Home/Home';
import Footer from './components/commons/Footer/Footer';
import About from './components/pages/Home/js/About';
import Contact from './components/pages/Home/js/Contact';
import './App.css';
import SignIn from './components/pages/Home/js/SignIn';
import Register from './components/pages/Home/js/Register';
import Logout from './components/pages/Home/js/Logout';
import Dashboard from './components/pages/Admin/Dashboard';

/*admin part*//*company management*/
import AllCompanies from './components/pages/Admin/CompanyManagement/AllCompanies';
import CompanyDetails from './components/pages/Admin/CompanyManagement/CompanyDetails';
import CreateCompany from './components/pages/Admin/CompanyManagement/CreateCompany';
import UpdateCompany from './components/pages/Admin/CompanyManagement/UpdateCompany';

/*admin part*//*user management*/
import AllUsers from './components/pages/Admin/UserManagement/AllUsers';
import CreateUser from './components/pages/Admin/UserManagement/CreateUser';
import UpdateUser from './components/pages/Admin/UserManagement/UpdateUser';
import UserDetails from './components/pages/Admin/UserManagement/UserDetails';

/*employer tasks*/
import CreateJobPostings from './components/pages/Employer/CreateJobPostings';
import UpdateJobPost from './components/pages/Employer/UpdateJobPost';
import ViewPosts from './components/pages/Employer/ViewPosts';
import ViewApplications from './components/pages/Employer/ViewApplications';

import JobPostings from './components/pages/User/JobPostings';

function App() {
  return (
    <Router>
      <div className="app-content">
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/admin" element={<SignIn />} />
          <Route path="/employer" element={<SignIn />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/signin" element={<SignIn />} />
          <Route path="/register" element={<Register />} />
          <Route path="/logout" element={<Logout />} />

          
          <Route path='/admin/dashboard' element={<Dashboard />}/>

          <Route path="/admin/companies" element={<AllCompanies />} />
          <Route path="/admin/companies/create" element={<CreateCompany />} />
          <Route path="/admin/companies/:companyId" element={<CompanyDetails />} />
          <Route path="/admin/companies/update/:companyId" element={<UpdateCompany />} />
          
          <Route path="/admin/users" element={<AllUsers />} />
          <Route path="/admin/users/create" element={<CreateUser />} />
          <Route path="/admin/users/:userId" element={<UserDetails />} />
          <Route path="/admin/users/edit/:userId" element={<UpdateUser />} />

          {/* Employer routes */}
          <Route path="/employer/job-postings/create" element={<CreateJobPostings />} />
          <Route path="/employer/job-postings/:jobId/update" element={<UpdateJobPost />} />
          <Route path="/employer/job-postings" element={<ViewPosts />} />
          <Route path="/employer/job-postings/:jobId/applications" element={<ViewApplications />} />

          {/* User routes */}
          <Route path="/job-postings" element={<JobPostings />} />
        </Routes>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
