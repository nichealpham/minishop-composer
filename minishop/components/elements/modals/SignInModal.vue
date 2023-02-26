<template>
    <div class="modal-body m-3">
        <button type="button" class="close" @click="$emit('close')">
            <span aria-hidden="true">
                <i class="icon-close"></i>
            </span>
        </button>

        <div class="form-box">
            <div class="form-tab">
                <tabs class="nav-pills nav-fill" :data="tabsData"></tabs>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="sign-in">
                        <form action="#">
                            <div class="form-group">
                                <label for="singin-email"
                                    >Username or email address *</label
                                >
                                <input
                                    type="text"
                                    class="form-control"
                                    id="singin-email"
                                    name="singin-email"
                                    required
                                />
                            </div>

                            <div class="form-group">
                                <label for="singin-password">Password *</label>
                                <input
                                    type="password"
                                    class="form-control"
                                    id="singin-password"
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
                                    <i class="icon-long-arrow-right"></i>
                                </button>

                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="signin-remember"
                                    />
                                    <label
                                        class="custom-control-label"
                                        for="signin-remember"
                                        >Remember Me</label
                                    >
                                </div>

                                <a href="javascript:;" class="forgot-link"
                                    >Forgot Your Password?</a
                                >
                            </div>
                        </form>
                    </div>

                    <div class="tab-pane fade" id="register">
                        <form action="#">
                            <div class="form-group">
                                <label for="register-email"
                                    >Your email address *</label
                                >
                                <input
                                    type="email"
                                    class="form-control"
                                    id="register-email"
                                    name="register-email"
                                    required
                                />
                            </div>

                            <div class="form-group">
                                <label for="register-password"
                                    >Password *</label
                                >
                                <input
                                    type="password"
                                    class="form-control"
                                    id="register-password"
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
                                    <i class="icon-long-arrow-right"></i>
                                </button>

                                <div class="custom-control custom-checkbox">
                                    <input
                                        type="checkbox"
                                        class="custom-control-input"
                                        id="register-policy"
                                        required
                                    />
                                    <label
                                        class="custom-control-label"
                                        for="register-policy"
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
                    id: 'sign-in',
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
