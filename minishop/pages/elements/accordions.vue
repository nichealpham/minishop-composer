<template>
    <main class="main">
        <page-header title="Accordions" subtitle="Elements"></page-header>

        <div class="page-content">
            <div class="container">
                <h2 class="title mb-3">Simple Accordions</h2>

                <div class="row">
                    <div class="col-md-6">
                        <div class="accordion" id="accordion-1">
                            <div class="card">
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
                                            >Cras ornare tristique elit.</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[0][0]">
                                    <div class="card-body">
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card">
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
                                            >Vivamus vestibulum ntulla</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[0][1]">
                                    <div class="card-body">
                                        Ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.Lorem ipsum dolor
                                        sit amet, consectetuer adipiscing elit.
                                        Donec odio. Quisque volutpat mattis
                                        eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card">
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
                                            >Praesent placerat risus</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[0][2]">
                                    <div class="card-body">
                                        Nullam malesuada erat ut turpis.
                                        Suspendisse urna nibh, viverra non,
                                        semper suscipit, posuere a, pede. Donec
                                        nec justo eget felis facilisis
                                        fermentum.Lorem ipsum dolor sit amet,
                                        consectetuer adipiscing elit. Donec
                                        odio. Quisque volutpat mattis eros.
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="accordion accordion-plus" id="accordion-2">
                            <div class="card">
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
                                            >Cras ornare tristique elit.</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[1][0]">
                                    <div class="card-body">
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card">
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
                                            >Vivamus vestibulum ntulla</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[1][1]">
                                    <div class="card-body">
                                        Ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.Lorem ipsum dolor
                                        sit amet, consectetuer adipiscing elit.
                                        Donec odio. Quisque volutpat mattis
                                        eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card">
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
                                            >Praesent placerat risus</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[1][2]">
                                    <div class="card-body">
                                        Nullam malesuada erat ut turpis.
                                        Suspendisse urna nibh, viverra non,
                                        semper suscipit, posuere a, pede. Donec
                                        nec justo eget felis facilisis
                                        fermentum.Lorem ipsum dolor sit amet,
                                        consectetuer adipiscing elit. Donec
                                        odio. Quisque volutpat mattis eros.
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-4" />

                <h2 class="title mb-3">Accordion with Icons</h2>

                <div class="row">
                    <div class="col-md-6">
                        <div class="accordion accordion-icon" id="accordion-3">
                            <div class="card">
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
                                        >
                                            <i class="icon-star-o"></i>Cras
                                            ornare tristique elit.
                                        </a>
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[2][0]">
                                    <div class="card-body">
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card">
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
                                        >
                                            <i class="icon-info-circle"></i
                                            >Vivamus vestibulum ntulla
                                        </a>
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[2][1]">
                                    <div class="card-body">
                                        Ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.Lorem ipsum dolor
                                        sit amet, consectetuer adipiscing elit.
                                        Donec odio. Quisque volutpat mattis
                                        eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card">
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
                                        >
                                            <i class="icon-heart-o"></i>Praesent
                                            placerat risus
                                        </a>
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[2][2]">
                                    <div class="card-body">
                                        Nullam malesuada erat ut turpis.
                                        Suspendisse urna nibh, viverra non,
                                        semper suscipit, posuere a, pede. Donec
                                        nec justo eget felis facilisis
                                        fermentum.Lorem ipsum dolor sit amet,
                                        consectetuer adipiscing elit. Donec
                                        odio. Quisque volutpat mattis eros.
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="accordion accordion-icon" id="accordion-4">
                            <div class="card card-box bg-light">
                                <div class="card-header" id="heading4-1">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[3][0],
                                                collapsed: !toggleState[3][0]
                                            }"
                                            @click.prevent="changeToggle(3, 0)"
                                            href="#"
                                        >
                                            <i class="icon-star-o"></i>Cras
                                            ornare tristique elit.
                                        </a>
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[3][0]">
                                    <div class="card-body">
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card card-box bg-light">
                                <div class="card-header" id="heading4-2">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[3][1],
                                                collapsed: !toggleState[3][1]
                                            }"
                                            @click.prevent="changeToggle(3, 1)"
                                            href="#"
                                        >
                                            <i class="icon-info-circle"></i
                                            >Vivamus vestibulum ntulla
                                        </a>
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[3][1]">
                                    <div class="card-body">
                                        Ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.Lorem ipsum dolor
                                        sit amet, consectetuer adipiscing elit.
                                        Donec odio. Quisque volutpat mattis
                                        eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card card-box bg-light">
                                <div class="card-header" id="heading4-3">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[3][2],
                                                collapsed: !toggleState[3][2]
                                            }"
                                            @click.prevent="changeToggle(3, 2)"
                                            href="#"
                                        >
                                            <i class="icon-heart-o"></i>Praesent
                                            placerat risus
                                        </a>
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[3][2]">
                                    <div class="card-body">
                                        Nullam malesuada erat ut turpis.
                                        Suspendisse urna nibh, viverra non,
                                        semper suscipit, posuere a, pede. Donec
                                        nec justo eget felis facilisis
                                        fermentum.Lorem ipsum dolor sit amet,
                                        consectetuer adipiscing elit. Donec
                                        odio. Quisque volutpat mattis eros.
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mb-4" />

                <h2 class="title mb-3">Accordions on Background</h2>

                <div class="row">
                    <div class="col-md-6">
                        <div
                            class="accordion accordion-rounded"
                            id="accordion-5"
                        >
                            <div class="card card-box card-sm bg-light">
                                <div class="card-header" id="heading5-1">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[4][0],
                                                collapsed: !toggleState[4][0]
                                            }"
                                            @click.prevent="changeToggle(4, 0)"
                                            href="#"
                                            >Cras ornare tristique elit.</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[4][0]">
                                    <div class="card-body">
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card card-box card-sm bg-light">
                                <div class="card-header" id="heading5-2">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[4][1],
                                                collapsed: !toggleState[4][1]
                                            }"
                                            @click.prevent="changeToggle(4, 1)"
                                            href="#"
                                            >Vivamus vestibulum ntulla</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[4][1]">
                                    <div class="card-body">
                                        Ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.Lorem ipsum dolor
                                        sit amet, consectetuer adipiscing elit.
                                        Donec odio. Quisque volutpat mattis
                                        eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card card-box card-sm bg-light">
                                <div class="card-header" id="heading5-3">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[4][2],
                                                collapsed: !toggleState[4][2]
                                            }"
                                            @click.prevent="changeToggle(4, 2)"
                                            href="#"
                                            >Praesent placerat risus</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[4][2]">
                                    <div class="card-body">
                                        Nullam malesuada erat ut turpis.
                                        Suspendisse urna nibh, viverra non,
                                        semper suscipit, posuere a, pede. Donec
                                        nec justo eget felis facilisis
                                        fermentum.Lorem ipsum dolor sit amet,
                                        consectetuer adipiscing elit. Donec
                                        odio. Quisque volutpat mattis eros.
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div
                            class="accordion accordion-rounded accordion-plus"
                            id="accordion-6"
                        >
                            <div class="card card-box card-sm bg-white">
                                <div class="card-header" id="heading6-1">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[5][0],
                                                collapsed: !toggleState[5][0]
                                            }"
                                            @click.prevent="changeToggle(5, 0)"
                                            href="#"
                                            >Cras ornare tristique elit.</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[5][0]">
                                    <div class="card-body">
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card card-box card-sm bg-white">
                                <div class="card-header" id="heading6-2">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[5][1],
                                                collapsed: !toggleState[5][1]
                                            }"
                                            @click.prevent="changeToggle(5, 1)"
                                            href="#"
                                            >Vivamus vestibulum ntulla</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[5][1]">
                                    <div class="card-body">
                                        Ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros. Nullam malesuada
                                        erat ut turpis. Suspendisse urna nibh,
                                        viverra non, semper suscipit, posuere a,
                                        pede. Donec nec justo eget felis
                                        facilisis fermentum.Lorem ipsum dolor
                                        sit amet, consectetuer adipiscing elit.
                                        Donec odio. Quisque volutpat mattis
                                        eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>

                            <div class="card card-box card-sm bg-white">
                                <div class="card-header" id="heading6-3">
                                    <h2 class="card-title">
                                        <a
                                            class="toggle-button"
                                            :class="{
                                                expanded: toggleState[5][2],
                                                collapsed: !toggleState[5][2]
                                            }"
                                            @click.prevent="changeToggle(5, 2)"
                                            href="#"
                                            >Praesent placerat risus</a
                                        >
                                    </h2>
                                </div>

                                <vue-slide-toggle :open="toggleState[5][2]">
                                    <div class="card-body">
                                        Nullam malesuada erat ut turpis.
                                        Suspendisse urna nibh, viverra non,
                                        semper suscipit, posuere a, pede. Donec
                                        nec justo eget felis facilisis
                                        fermentum.Lorem ipsum dolor sit amet,
                                        consectetuer adipiscing elit. Donec
                                        odio. Quisque volutpat mattis eros.
                                        Lorem ipsum dolor sit amet, consectetuer
                                        adipiscing elit. Donec odio. Quisque
                                        volutpat mattis eros.
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import { VueSlideToggle } from 'vue-slide-toggle';
import PageHeader from '~/components/elements/PageHeader';
import ElementList from '~/components/partial/elements/ElementList';

export default {
    components: {
        VueSlideToggle,
        PageHeader,
        ElementList
    },
    data: function() {
        return {
            toggleState: [
                [true, false, false],
                [true, false, false],
                [true, false, false],
                [true, false, false],
                [true, false, false],
                [true, false, false]
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
