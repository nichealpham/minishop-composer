<template>
    <main class="main">
        <page-header title="My Account" subtitle="Shop"></page-header>

        <div class="page-content">
            <div class="dashboard">
                <div class="container">
                    <div class="row">
                        <aside class="col-md-4 col-lg-3">
                            <tabs
                                class="nav-dashboard flex-column mb-3 mb-md-0"
                                :data="tabsData"
                            ></tabs>
                        </aside>

                        <div class="col-md-8 col-lg-9">
                            <div class="tab-content">
                                <div
                                    class="tab-pane fade show active"
                                    id="tab1"
                                >
                                    <p>
                                        Hello
                                        <span
                                            class="font-weight-normal text-dark"
                                            >User</span
                                        >
                                        (not
                                        <span
                                            class="font-weight-normal text-dark"
                                            >User</span
                                        >? <a href="#">Log out</a>) <br />From
                                        your account dashboard you can view your
                                        <a
                                            href="#tab-orders"
                                            class="tab-trigger-link link-underline"
                                            @click.prevent="toOrder"
                                            >recent orders</a
                                        >, manage your
                                        <a
                                            href="#tab-address"
                                            class="tab-trigger-link"
                                            @click.prevent="toAddress"
                                            >shipping and billing addresses</a
                                        >, and
                                        <a
                                            href="#tab-account"
                                            class="tab-trigger-link"
                                            @click.prevent="toAccount"
                                            >edit your password and account
                                            details</a
                                        >.
                                    </p>
                                </div>

                                <div class="tab-pane fade" id="tab2">
                                    <p>No order has been made yet.</p>
                                    <nuxt-link
                                        :to="'/shop/sidebar/3cols'"
                                        class="btn btn-outline-primary-2"
                                    >
                                        <span>GO SHOP</span>
                                        <i class="icon-long-arrow-right"></i>
                                    </nuxt-link>
                                </div>

                                <div class="tab-pane fade" id="tab3">
                                    <p>No downloads available yet.</p>
                                    <nuxt-link
                                        :to="'/shop/sidebar/3cols'"
                                        class="btn btn-outline-primary-2"
                                    >
                                        <span>GO SHOP</span>
                                        <i class="icon-long-arrow-right"></i>
                                    </nuxt-link>
                                </div>

                                <div class="tab-pane fade" id="tab4">
                                    <p>
                                        The following addresses will be used on
                                        the checkout page by default.
                                    </p>

                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="card card-dashboard">
                                                <div class="card-body">
                                                    <h3 class="card-title">
                                                        Billing Address
                                                    </h3>

                                                    <p>
                                                        User Name
                                                        <br />User Company
                                                        <br />John str <br />New
                                                        York, NY 10001
                                                        <br />1-234-987-6543
                                                        <br />yourmail@mail.com
                                                        <br />
                                                        <a href="#">
                                                            Edit
                                                            <i
                                                                class="icon-edit"
                                                            ></i>
                                                        </a>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-6">
                                            <div class="card card-dashboard">
                                                <div class="card-body">
                                                    <h3 class="card-title">
                                                        Shipping Address
                                                    </h3>

                                                    <p>
                                                        You have not set up this
                                                        type of address yet.
                                                        <br />
                                                        <a href="#">
                                                            Edit
                                                            <i
                                                                class="icon-edit"
                                                            ></i>
                                                        </a>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="tab5">
                                    <form action="#">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label>First Name *</label>
                                                <input
                                                    type="text"
                                                    class="form-control"
                                                    required
                                                />
                                            </div>

                                            <div class="col-sm-6">
                                                <label>Last Name *</label>
                                                <input
                                                    type="text"
                                                    class="form-control"
                                                    required
                                                />
                                            </div>
                                        </div>

                                        <label>Display Name *</label>
                                        <input
                                            type="text"
                                            class="form-control"
                                            required
                                        />
                                        <small class="form-text"
                                            >This will be how your name will be
                                            displayed in the account section and
                                            in reviews</small
                                        >

                                        <label>Email address *</label>
                                        <input
                                            type="email"
                                            class="form-control"
                                            required
                                        />

                                        <label
                                            >Current password (leave blank to
                                            leave unchanged)</label
                                        >
                                        <input
                                            type="password"
                                            class="form-control"
                                        />

                                        <label
                                            >New password (leave blank to leave
                                            unchanged)</label
                                        >
                                        <input
                                            type="password"
                                            class="form-control"
                                        />

                                        <label>Confirm new password</label>
                                        <input
                                            type="password"
                                            class="form-control mb-2"
                                        />

                                        <button
                                            type="submit"
                                            class="btn btn-outline-primary-2"
                                        >
                                            <span>SAVE CHANGES</span>
                                            <i
                                                class="icon-long-arrow-right"
                                            ></i>
                                        </button>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import PageHeader from '~/components/elements/PageHeader';
import Tabs from '~/components/elements/Tabs';

export default {
    components: {
        Tabs,
        PageHeader
    },
    data: function() {
        return {
            tabsData: [
                {
                    id: 'tab1',
                    title: 'Dashboard',
                    active: true
                },
                {
                    id: 'tab2',
                    title: 'Orders'
                },
                {
                    id: 'tab3',
                    title: 'Downloads'
                },
                {
                    id: 'tab4',
                    title: 'Addresses'
                },
                {
                    id: 'tab5',
                    title: 'Account Details'
                },
                {
                    id: 'tab6',
                    title: 'Sign Out'
                }
            ]
        };
    },
    methods: {
        toOrder: function() {
            document
                .querySelector('.nav-dashboard .nav-item:nth-child(2) a')
                .click();
        },
        toAddress: function() {
            document
                .querySelector('.nav-dashboard .nav-item:nth-child(4) a')
                .click();
        },
        toAccount: function() {
            document
                .querySelector('.nav-dashboard .nav-item:nth-child(5) a')
                .click();
        }
    }
};
</script>
