<template>
    <main class="main">
        <page-header title="Titles" subtitle="Elements"></page-header>

        <div class="page-content">
            <div class="container">
                <h2 class="title">Simple title</h2>

                <p>
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>

                <hr class="mt-4 mb-4" />

                <div class="heading">
                    <h2 class="title">
                        Simple title
                        <span class="title-separator">/</span> Subtitle
                    </h2>

                    <p class="title-desc">
                        Donec consectetuer ligula vulputate sem tristique
                        cursus.
                    </p>
                </div>

                <p>
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>

                <hr class="mt-4 mb-4" />

                <div class="heading heading-flex">
                    <div class="heading-left">
                        <h2 class="title">
                            Simple title
                            <span class="title-separator">/</span> Product
                            Filter
                        </h2>
                    </div>

                    <div class="heading-right">
                        <tabs
                            class="nav-pills justify-content-center"
                            id="tab-1"
                            :data="tabsData"
                        ></tabs>
                    </div>
                </div>

                <div class="tab-content">
                    <div
                        class="tab-pane p-0 fade show active"
                        id="tab-1-product"
                    >
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-1-women">
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-1-men">
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-1-accessories">
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                </div>

                <hr class="mt-4 mb-4" />

                <div class="heading heading-flex align-items-start">
                    <div class="heading-left">
                        <h2 class="title">
                            Simple title
                            <span class="title-separator">/</span> Subtitle
                            <span class="title-separator">/</span> Product
                            Filter
                        </h2>

                        <p class="title-desc">
                            Donec consectetuer ligula vulputate sem tristique
                            cursus.
                        </p>
                    </div>

                    <div class="heading-right">
                        <tabs
                            class="nav-pills justify-content-center"
                            id="tab-2"
                            :data="tabsData"
                        ></tabs>
                    </div>
                </div>

                <div class="tab-content">
                    <div
                        class="tab-pane p-0 fade show active"
                        id="tab-2-product"
                    >
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-2-women">
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-2-men">
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-2-accessories">
                        <p>
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                </div>

                <hr class="mt-4 mb-4" />

                <div class="heading heading-flex">
                    <div class="heading-left">
                        <h2 class="title">
                            Simple title
                            <span class="title-separator">/</span> Link
                        </h2>
                    </div>

                    <div class="heading-right">
                        <a href="#" class="title-link">
                            Click here to view
                            <i class="icon-long-arrow-right"></i>
                        </a>
                    </div>
                </div>

                <p>
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>

                <hr class="mt-4 mb-4" />

                <div class="heading heading-flex">
                    <div class="heading-left">
                        <h2 class="title">
                            Simple title
                            <span class="title-separator">/</span> Substitle
                            <span class="title-separator">/</span> Link
                        </h2>

                        <p class="title-desc">
                            Donec consectetuer ligula vulputate sem tristique
                            cursus.
                        </p>
                    </div>

                    <div class="heading-right">
                        <a href="#" class="title-link link-underline">
                            Click here to view
                            <i class="icon-long-arrow-right"></i>
                        </a>
                    </div>
                </div>

                <p>
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>

                <hr class="mt-4 mb-4" />

                <div class="text-center">
                    <h2 class="title">Centered title</h2>

                    <p>
                        Sed egestas, ante et vulputate volutpat, eros pede
                        semper est, vitae luctus metus libero eu augue. Morbi
                        purus libero, faucibus adipiscing, commodo quis, gravida
                        id, est. Sed lectus. Praesent elementum hendrerit
                        tortor. Sed semper lorem at felis. Vestibulum volutpat,
                        lacus a ultrices sagittis, mi neque euismod dui, eu
                        pulvinar nunc sapien ornare nisl. Phasellus pede arcu,
                        dapibus eu, fermentum et, dapibus sed, urna.
                    </p>
                </div>

                <hr class="mt-4 mb-4" />

                <div class="heading heading-center">
                    <div class="heading-left">
                        <h2 class="title">
                            Centered title
                            <span class="title-separator">/</span> Product
                            Filter
                        </h2>
                    </div>

                    <div class="heading-right">
                        <tabs
                            class="nav-pills justify-content-center"
                            id="tab-3"
                            :data="tabsData"
                        ></tabs>
                    </div>
                </div>

                <div class="tab-content">
                    <div
                        class="tab-pane p-0 fade show active"
                        id="tab-3-product"
                    >
                        <p class="text-center">
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-3-women">
                        <p class="text-center">
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-3-men">
                        <p class="text-center">
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                    <div class="tab-pane p-0 fade" id="tab-3-accessories">
                        <p class="text-center">
                            Sed egestas, ante et vulputate volutpat, eros pede
                            semper est, vitae luctus metus libero eu augue.
                            Morbi purus libero, faucibus adipiscing, commodo
                            quis, gravida id, est. Sed lectus. Praesent
                            elementum hendrerit tortor. Sed semper lorem at
                            felis. Vestibulum volutpat, lacus a ultrices
                            sagittis, mi neque euismod dui, eu pulvinar nunc
                            sapien ornare nisl. Phasellus pede arcu, dapibus eu,
                            fermentum et, dapibus sed, urna.
                        </p>
                    </div>
                </div>
                <hr class="mt-4 mb-4" />

                <h2 class="title-sm">Small Title</h2>

                <p>
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>

                <hr class="mt-4 mb-4" />

                <div class="heading">
                    <p class="title-desc mb-1">Quisque a lectus.</p>

                    <h2 class="title-sm">
                        Small title
                        <span class="title-separator">/</span> Subtitle
                    </h2>
                </div>

                <p>
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>

                <hr class="mt-4 mb-4" />

                <div class="text-center">
                    <h2 class="title-sm">Centered Small Title</h2>

                    <p>
                        Sed egestas, ante et vulputate volutpat, eros pede
                        semper est, vitae luctus metus libero eu augue. Morbi
                        purus libero, faucibus adipiscing, commodo quis, gravida
                        id, est. Sed lectus. Praesent elementum hendrerit
                        tortor. Sed semper lorem at felis. Vestibulum volutpat,
                        lacus a ultrices sagittis, mi neque euismod dui, eu
                        pulvinar nunc sapien ornare nisl. Phasellus pede arcu,
                        dapibus eu, fermentum et, dapibus sed, urna.
                    </p>
                </div>

                <hr class="mt-4 mb-4" />

                <div class="heading text-center">
                    <p class="title-desc mb-1">Quisque a lectus.</p>

                    <h2 class="title-sm">
                        Centered Small title
                        <span class="title-separator">/</span> Subtitle
                    </h2>
                </div>

                <p class="text-center">
                    Sed egestas, ante et vulputate volutpat, eros pede semper
                    est, vitae luctus metus libero eu augue. Morbi purus libero,
                    faucibus adipiscing, commodo quis, gravida id, est. Sed
                    lectus. Praesent elementum hendrerit tortor. Sed semper
                    lorem at felis. Vestibulum volutpat, lacus a ultrices
                    sagittis, mi neque euismod dui, eu pulvinar nunc sapien
                    ornare nisl. Phasellus pede arcu, dapibus eu, fermentum et,
                    dapibus sed, urna.
                </p>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import PageHeader from '~/components/elements/PageHeader';
import Tabs from '~/components/elements/Tabs';
import ElementList from '~/components/partial/elements/ElementList';

export default {
    components: {
        Tabs,
        PageHeader,
        ElementList
    },
    data: function() {
        return {
            tabsData: [
                {
                    id: 'product',
                    title: 'All Products',
                    active: true
                },
                {
                    id: 'women',
                    title: 'Women'
                },
                {
                    id: 'men',
                    title: 'Men'
                },
                {
                    id: 'accessories',
                    title: 'Accessories'
                }
            ]
        };
    }
};
</script>
