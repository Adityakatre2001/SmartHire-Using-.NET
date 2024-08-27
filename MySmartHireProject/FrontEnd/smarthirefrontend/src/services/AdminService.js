import axios from 'axios';

const baseurl = 'http://localhost:8080/admin';

class AdminService {
    listAllUsers() {
        return axios.get(baseurl + '/users');
    }

    getUserById(userId) {
        return axios.get(baseurl + `/users/${userId}`);
    }

    createUser(user) {
        return axios.post(baseurl + '/users', user);
    }

    updateUser(userId, user) {
        return axios.put(baseurl + `/users/${userId}`, user);
    }

    deleteUser(userId) {
        return axios.delete(baseurl + `/users/${userId}`);
    }

    getAllCompanies() {
        return axios.get(baseurl + '/companies');
    }

    getCompanyById(companyId) {
        return axios.get(baseurl + `/companies/${companyId}`);
    }

    createCompany(company) {
        return axios.post(baseurl + '/companies', company);
    }

    updateCompany(companyId, company) {
        return axios.put(baseurl + `/companies/${companyId}`, company);
    }

    deleteCompany(companyId) {
        return axios.delete(baseurl + `/companies/${companyId}`);
    }
}

export default new AdminService();
