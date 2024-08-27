const initialState = {
    token: null,
    role: null,
  };
  
  const authReducer = (state = initialState, action) => {
    switch (action.type) {
      case 'SET_USER':
        return {
          ...state,
          token: action.payload.token,
          role: action.payload.role,
        };
      case 'LOGOUT_USER':
        return {
          ...state,
          token: null,
          role: null,
        };
      default:
        return state;
    }
  };
  
  export default authReducer;
  