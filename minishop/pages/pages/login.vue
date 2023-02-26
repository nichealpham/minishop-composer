<template>
    <main class="main">
        <div
            class="login-page bg-image pt-8 pb-8 pt-md-12 pb-md-12 pt-lg-17 pb-lg-17"
            v-lazy:background-image="'./images/backgrounds/login-bg.jpg'"
        >
            <div class="container">
                <div class="form-box">
                    <div class="form-tab">
                        <tabs
                            class="nav-pills nav-fill"
                            :data="tabsData"
                        ></tabs>
                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="sign_in">
                                <form action="#">
                                    <div class="form-group">
                                        <label for="singin-email-2"
                                            >Username or email address *</label
                                        >
                                        <input
                                            type="text"
                                            class="form-control"
                                            id="singin-email-2"
                                            name="singin-email"
                                            required
                                        />
                                    </div>

                                    <div class="form-group">
                                        <label for="singin-password-2"
                                            >Password *</label
                                        >
                                        <input
                                            type="password"
                                            class="form-control"
                                            id="singin-password-2"
                                            name="singin-password"
                                            required
                                        />
                                    </div>

                                    <div class="form-footer">
                                        <button
                                            type="submit"
                                            class="btn btn-outline-primary-2"
                                        >
                                            <span>LOG IN</span>
                                            <i
                                                class="icon-long-arrow-right"
                                            ></i>
                                        </button>

                                        <div
                                            class="custom-control custom-checkbox"
                                        >
                                            <input
                                                type="checkbox"
                                                class="custom-control-input"
                                                id="signin-remember-2"
                                            />
                                            <label
                                                class="custom-control-label"
                                                for="signin-remember-2"
                                                >Remember Me</label
                                            >
                                        </div>

                                        <a
                                            href="javascript:;"
                                            class="forgot-link"
                                            >Forgot Your Password?</a
                                        >
                                    </div>
                                </form>
                            </div>

                            <div class="tab-pane fade" id="register">
                                <form action="#">
                                    <div class="form-group">
                                        <label for="register-email-2"
                                            >Your email address *</label
                                        >
                                        <input
                                            type="email"
                                            class="form-control"
                                            id="register-email-2"
                                            name="register-email"
                                            required
                                        />
                                    </div>

                                    <div class="form-group">
                                        <label for="register-password-2"
                                            >Password *</label
                                        >
                                        <input
                                            type="password"
                                            class="form-control"
                                            id="register-password-2"
                                            name="register-password"
                                            required
                                        />
                                    </div>

                                    <div class="form-footer">
                                        <button
                                            type="submit"
                                            class="btn btn-outline-primary-2"
                                        >
                                            <span>SIGN UP</span>
                                            <i
                                                class="icon-long-arrow-right"
                                            ></i>
                                        </button>

                                        <div
                                            class="custom-control custom-checkbox"
                                        >
                                            <input
                                                type="checkbox"
                                                class="custom-control-input"
                                                id="register-policy-2"
                                                required
                                            />
                                            <label
                                                class="custom-control-label"
                                                for="register-policy-2"
                                            >
                                                I agree to the
                                                <a href="javascript:;"
                                                    >privacy policy</a
                                                >
                                                *
                                            </label>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import Tabs from '~/components/elements/Tabs';
export default {
    components: {
        Tabs
    },
    data: function() {
        return {
            tabsData: [
                {
                    id: 'sign_in',
                    title: 'Sign In',
                    active: true
                },
                {
                    id: 'register',
                    title: 'Register'
                }
            ]
        };
    }
};
</script>
