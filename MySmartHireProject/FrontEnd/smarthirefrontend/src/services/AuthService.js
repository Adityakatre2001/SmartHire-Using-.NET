
// import axios from 'axios';

// const baseurl = 'http://localhost:8080/auth/';

// class AuthService {
//     login(email, password) {
//         return axios
//             .post(API_URL + 'signin', { email, password })
//             .then(response => {
//                 if (response.data.token) {
//                     localStorage.setItem('user', JSON.stringify(response.data));
//                 }
//                 return response.data;
//             });
//     }

//     logout() {
//         localStorage.removeItem('user');
//     }

//     getCurrentUser() {
//         return JSON.parse(localStorage.getItem('user'));
//     }

//     isAuthenticated() {
//         return !!this.getCurrentUser();
//     }
// }

// export default new AuthService();
