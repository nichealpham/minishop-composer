<template>
    <main class="main">
        <page-header title="F.A.Q" subtitle="Pages"></page-header>

        <div class="page-content">
            <div class="container">
                <h2 class="title text-center mb-3">Shipping Information</h2>

                <div class="accordion accordion-rounded" id="accordion-1">
                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading-1">
                            <h2 class="card-title">
                                <a
                                    href="#"
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[0][0],
                                        collapsed: !toggleState[0][0]
                                    }"
                                    @click.prevent="changeToggle(0, 0)"
                                    >How will my parcel be delivered?</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[0][0]">
                            <div class="card-body">
                                Lorem ipsum dolor sit amet, consectetuer
                                adipiscing elit. Donec odio. Quisque volutpat
                                mattis eros. Nullam malesuada erat ut turpis.
                                Suspendisse urna nibh, viverra non, semper
                                suscipit, posuere a, pede. Donec nec justo eget
                                felis facilisis fermentum.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading-2">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[0][1],
                                        collapsed: !toggleState[0][1]
                                    }"
                                    @click.prevent="changeToggle(0, 1)"
                                    href="#"
                                    >Do I pay for delivery?</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[0][1]">
                            <div class="card-body">
                                Ipsum dolor sit amet, consectetuer adipiscing
                                elit. Donec odio. Quisque volutpat mattis eros.
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading-3">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[0][2],
                                        collapsed: !toggleState[0][2]
                                    }"
                                    @click.prevent="changeToggle(0, 2)"
                                    href="#"
                                    >Will I be charged customs fees?</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[0][2]">
                            <div class="card-body">
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros. Lorem ipsum dolor
                                sit amet, consectetuer adipiscing elit. Donec
                                odio. Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading-4">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[0][3],
                                        collapsed: !toggleState[0][3]
                                    }"
                                    @click.prevent="changeToggle(0, 3)"
                                    href="#"
                                    >My item has become faulty</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[0][3]">
                            <div class="card-body">
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros. Lorem ipsum dolor
                                sit amet, consectetuer adipiscing elit. Donec
                                odio. Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>
                </div>

                <h2 class="title text-center mb-3">Orders and Returns</h2>

                <div class="accordion accordion-rounded" id="accordion-2">
                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading2-1">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[1][0],
                                        collapsed: !toggleState[1][0]
                                    }"
                                    @click.prevent="changeToggle(1, 0)"
                                    href="#"
                                    >Tracking my order</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[1][0]">
                            <div class="card-body">
                                Lorem ipsum dolor sit amet, consectetuer
                                adipiscing elit. Donec odio. Quisque volutpat
                                mattis eros. Nullam malesuada erat ut turpis.
                                Suspendisse urna nibh, viverra non, semper
                                suscipit, posuere a, pede. Donec nec justo eget
                                felis facilisis fermentum.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading2-2">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[1][1],
                                        collapsed: !toggleState[1][1]
                                    }"
                                    @click.prevent="changeToggle(1, 1)"
                                    href="#"
                                    >I haven’t received my order</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[1][1]">
                            <div class="card-body">
                                Ipsum dolor sit amet, consectetuer adipiscing
                                elit. Donec odio. Quisque volutpat mattis eros.
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading2-3">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[1][2],
                                        collapsed: !toggleState[1][2]
                                    }"
                                    @click.prevent="changeToggle(1, 2)"
                                    href="#"
                                    >How can I return an item?</a
                                >
                            </h2>
                        </div>
                        <vue-slide-toggle :open="toggleState[1][2]">
                            <div class="card-body">
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros. Lorem ipsum dolor
                                sit amet, consectetuer adipiscing elit. Donec
                                odio. Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>
                </div>

                <h2 class="title text-center mb-3">Payments</h2>

                <div class="accordion accordion-rounded" id="accordion-3">
                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading3-1">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[2][0],
                                        collapsed: !toggleState[2][0]
                                    }"
                                    @click.prevent="changeToggle(2, 0)"
                                    href="#"
                                    >What payment types can I use?</a
                                >
                            </h2>
                        </div>
                        <vue-slide-toggle :open="toggleState[2][0]">
                            <div class="card-body">
                                Lorem ipsum dolor sit amet, consectetuer
                                adipiscing elit. Donec odio. Quisque volutpat
                                mattis eros. Nullam malesuada erat ut turpis.
                                Suspendisse urna nibh, viverra non, semper
                                suscipit, posuere a, pede. Donec nec justo eget
                                felis facilisis fermentum.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading3-2">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[2][1],
                                        collapsed: !toggleState[2][1]
                                    }"
                                    @click.prevent="changeToggle(2, 1)"
                                    href="#"
                                    >Can I pay by Gift Card?</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[2][1]">
                            <div class="card-body">
                                Ipsum dolor sit amet, consectetuer adipiscing
                                elit. Donec odio. Quisque volutpat mattis eros.
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading3-3">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[2][2],
                                        collapsed: !toggleState[2][2]
                                    }"
                                    @click.prevent="changeToggle(2, 2)"
                                    href="#"
                                    >I can't make a payment</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[2][2]">
                            <div class="card-body">
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros. Lorem ipsum dolor
                                sit amet, consectetuer adipiscing elit. Donec
                                odio. Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>

                    <div class="card card-box card-sm bg-light">
                        <div class="card-header" id="heading3-4">
                            <h2 class="card-title">
                                <a
                                    class="toggle-button"
                                    :class="{
                                        expanded: toggleState[2][3],
                                        collapsed: !toggleState[2][3]
                                    }"
                                    @click.prevent="changeToggle(2, 3)"
                                    href="#"
                                    >Has my payment gone through?</a
                                >
                            </h2>
                        </div>

                        <vue-slide-toggle :open="toggleState[2][3]">
                            <div class="card-body">
                                Nullam malesuada erat ut turpis. Suspendisse
                                urna nibh, viverra non, semper suscipit, posuere
                                a, pede. Donec nec justo eget felis facilisis
                                fermentum.Lorem ipsum dolor sit amet,
                                consectetuer adipiscing elit. Donec odio.
                                Quisque volutpat mattis eros. Lorem ipsum dolor
                                sit amet, consectetuer adipiscing elit. Donec
                                odio. Quisque volutpat mattis eros.
                            </div>
                        </vue-slide-toggle>
                    </div>
                </div>
            </div>
        </div>

        <div
            class="cta cta-display bg-image pt-4 pb-4"
            v-lazy:background-image="'./images/backgrounds/cta/bg-7.jpg'"
        >
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-10 col-lg-9 col-xl-7">
                        <div
                            class="row no-gutters flex-column flex-sm-row align-items-sm-center"
                        >
                            <div class="col">
                                <h3 class="cta-title text-white">
                                    If You Have More Questions
                                </h3>

                                <p class="cta-desc text-white">
                                    Quisque volutpat mattis eros
                                </p>
                            </div>

                            <div class="col-auto">
                                <nuxt-link
                                    to="/pages/contact"
                                    class="btn btn-outline-white"
                                >
                                    <span>CONTACT US</span>
                                    <i class="icon-long-arrow-right"></i>
                                </nuxt-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import { VueSlideToggle } from 'vue-slide-toggle';
import PageHeader from '~/components/elements/PageHeader';

export default {
    components: {
        VueSlideToggle,
        PageHeader
    },
    data: function() {
        return {
            toggleState: [
                [false, false, false, false],
                [false, false, false],
                [false, false, false, false]
            ]
        };
    },
    methods: {
        changeToggle: function(index1, index2) {
            this.toggleState = this.toggleState.reduce((acc1, cur1, id1) => {
                if (id1 == index1) {
                    let newStates = cur1.reduce((acc2, cur2, id2) => {
                        if (id2 == index2) return [...acc2, !cur2];
                        else return [...acc2, false];
                    }, []);

                    return [...acc1, newStates];
                } else {
                    return [...acc1, cur1];
                }
            }, []);
        }
    }
};
</script>
